using Microsoft.AspNetCore.Identity;

namespace E_CommerceProject.Entities.Models
{
    public class AppUser : IdentityUser
    {
        public int? CustomerInfoId { get; set; }
        public CustomerInfo? CustomerInfo { get; set; }
    }
}
