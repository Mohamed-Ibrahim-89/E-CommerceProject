namespace E_CommerceProject.Entities.ViewModels.Products;

public class ProductDetailsViewModel
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string Cover { get; set; } = string.Empty;

    [Display(Name = "Quantity")]
    public int QuantityInStock { get; set; }

    public string Category { get; set; } = string.Empty;

    public decimal Discount { get; set; }
}
