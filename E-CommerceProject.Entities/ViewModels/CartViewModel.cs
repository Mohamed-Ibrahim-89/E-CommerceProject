namespace E_CommerceProject.Entities.ViewModels;

public class CartViewModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;

    public string ProductCover { get; set; } = string.Empty;

    public decimal ProductPrice { get; set; }

    public decimal ProductDiscount { get; set; }

    public int Amount { get; set; }

    public decimal CartTotal { get; set; }

}
