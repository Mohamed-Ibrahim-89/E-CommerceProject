namespace E_CommerceProject.Entities.ViewModels.Customers;

public class CustomerInfoViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Date Of Birth")]
    public DateTime? DateOfBirth { get; set; }

    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Display(Name = "Address Line 1")]
    public string AddressLine1 { get; set; } = string.Empty;

    [Display(Name = "Address Line 2")]
    public string? AddressLine2 { get; set; }

    public string Country { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string? Landmark { get; set; }

    public string? ZipCode { get; set; }

    public string? State { get; set; }
}
