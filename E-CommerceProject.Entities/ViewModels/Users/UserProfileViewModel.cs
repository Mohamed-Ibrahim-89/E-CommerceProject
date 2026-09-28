namespace E_CommerceProject.Entities.ViewModels.Users;

public class UserProfileViewModel
{
    public string Id { get; set; } = string.Empty;

    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

}
