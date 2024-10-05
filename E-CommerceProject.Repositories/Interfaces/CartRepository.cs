using E_CommerceProject.Entities.Models;
using E_CommerceProject.Repositories.Data;
using E_CommerceProject.Repositories.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace E_CommerceProject.Repositories.Interfaces
{
    public class CartRepository(AppDbContext context) : ICartRepository
    {
        private readonly AppDbContext _context = context;
        public string? ShoppingCartId { get; set; }

        public static CartRepository GetCart(IServiceProvider serviceProvider)
        {
            // Retrieve the session from the IHttpContextAccessor
            ISession? session = serviceProvider.GetRequiredService<IHttpContextAccessor>()
                ?.HttpContext?.Session;

            // Get the AppDbContext from the service provider
            AppDbContext db = serviceProvider.GetService<AppDbContext>() ?? throw new Exception("Error Initializing");

            // Check if a CartId exists in the session, if not, create a new one
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            // Return a new instance of CartRepository with the ShoppingCartId set
            return new CartRepository(db) { ShoppingCartId = cartId };
        }

        public async Task AddToCart(Product product)
        {
            var CartItem = await _context.Carts.FirstOrDefaultAsync(
                s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId
            );

            if (CartItem == null)
            {
                CartItem = new Cart
                {
                    ShoppingCartId = ShoppingCartId,
                    ProductId = product.ProductId,
                    Amount = 1
                };
                _context.Carts.Add(CartItem);
            }
            else
            {
                CartItem.Amount++;
            }

            await _context.SaveChangesAsync();
        }

        public async Task ClearCart()
        {
            var cartItems = await _context.Carts.Where(cart => cart.ShoppingCartId == ShoppingCartId).ToListAsync();

            _context.Carts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cart>> GetCartItems()
        {
            return await _context.Carts.Where(
                c => c.ShoppingCartId == ShoppingCartId).Include(p => p.Product).ThenInclude(d => d.Discount).ToListAsync();

        }

        public async Task<decimal> GetCartTotal()
        {
            var total = await _context.Carts.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.Price * c.Amount).SumAsync();

            return total;
        }

        public async Task RemoveFromCart(Product product)
        {
            var CartItem = await _context.Carts.FirstOrDefaultAsync(
                s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId
                );

            var localAmount = 0;

            if (CartItem != null)
            {
                if (CartItem.Amount > 1)
                {
                    CartItem.Amount--;
                    localAmount = CartItem.Amount;
                }
                else
                {
                    _context.Carts.Remove(CartItem);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
