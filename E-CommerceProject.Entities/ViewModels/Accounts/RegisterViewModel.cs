namespace E_CommerceProject.Entities.ViewModels.Accounts;

public class RegisterViewModel
{
    [Required, StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required, StringLength(150), EmailAddress(ErrorMessage = "Email not valid")]
    public string Email { get; set; } = string.Empty;

    [Required, DataType(DataType.Password)]
    [Length(8, 150,ErrorMessage = "Password must be between 8 and 150 characters")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
    public string Password { get; set; } = string.Empty;

    [Required, DataType(DataType.Password)
        , Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; } = false;


}
