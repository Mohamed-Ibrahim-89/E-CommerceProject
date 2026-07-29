namespace E_CommerceProject.Controllers;

public class HomeController(IBaseRepository<Product> productRepository) : Controller
{
    private readonly IBaseRepository<Product> _productRepository = productRepository;

    public async Task<IActionResult> Index(string? categoryName)
    {
        if (categoryName != null)
        {
            var SelectedProducts = await _productRepository.GetAll(c => c.Category!.Name.Contains(categoryName), ["Category", "Discount"]);

            return View(SelectedProducts);
        }

        var products = await _productRepository.GetAll(null, ["Category", "Discount"]);

        return View(products);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Search(string searchQuery)
    {
        IEnumerable<Product> products;

        if (searchQuery != null)
        {
            ViewBag.SearchQuery = searchQuery;
            products = await _productRepository.GetAll(p => p.Name.Contains(searchQuery), ["Category", "Discount"]);
        }
        else
        {
            products = await _productRepository.GetAll(null, ["Category", "Discount"]);
        }

        return PartialView("_ProductCard", products);
    }
}
