namespace E_CommerceProject.Entities.ViewModels.Users;

public class UserDetailsViewModel
{
    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

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
