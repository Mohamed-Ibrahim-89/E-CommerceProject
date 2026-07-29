namespace E_CommerceProject.Entities.ViewModels.Products;

public class ProductViewModel
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    [Display(Name = "Quantity")]
    public int QuantityInStock { get; set; }

    [Display(Name = "Created At")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string Category { get; set; } = string.Empty;

    public decimal Discount { get; set; }
    public IFormFile File { get; set; } = default!;

}
