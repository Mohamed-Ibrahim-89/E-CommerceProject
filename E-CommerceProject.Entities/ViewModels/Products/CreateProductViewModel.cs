namespace E_CommerceProject.Entities.ViewModels.Products;

public class CreateProductViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public string Cover { get; set; } = string.Empty;

    [Display(Name = "Quantity")]
    public int QuantityInStock { get; set; }

    [Display(Name = "Created At")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string Category { get; set; } = string.Empty;

    public decimal Discount { get; set; }

    public int CategoryId { get; set; }
    public int DiscountId { get; set; }

    public IFormFile File { get; set; } = default!;
}
