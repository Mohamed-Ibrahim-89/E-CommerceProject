namespace E_CommerceProject.Entities.ViewModels;

public class WishListViewModel(List<Wishlist> wishListItem)
{
    public List<Wishlist> WishListItem { get; } = wishListItem;
}
