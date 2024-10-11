using E_CommerceProject.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Entities.ViewModels
{
    public class WishListViewModel(List<Wishlist> wishListItem)
    {
        public List<Wishlist> WishListItem { get; } = wishListItem;
    }
}
