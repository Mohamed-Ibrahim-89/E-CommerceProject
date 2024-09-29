using E_CommerceProject.Entities.Models;
using Microsoft.AspNetCore.Http;

namespace E_CommerceProject.Entities.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; } = default!;
        public IFormFile File { get; set; } = default!;
        public IEnumerable<Category>? Categories { get; set; }
    }
}
