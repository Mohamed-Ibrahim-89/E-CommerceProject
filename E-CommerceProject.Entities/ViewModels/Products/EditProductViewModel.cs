using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace E_CommerceProject.Entities.ViewModels.Products;

public class EditProductViewModel
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Cover { get; set; } = string.Empty;
    [Display(Name = "Quantity")]
    public int QuantityInStock { get; set; }
    [Display(Name = "Created At")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int CategoryId { get; set; }
    public int DiscountId { get; set; }
    public IFormFile? File { get; set; }
}
