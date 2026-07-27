namespace E_CommerceProject.Entities.ViewModels;

public class ChangePasswordViewModel
{
    [DataType(DataType.Password)]
    [Display(Name = "Cusrrent Password")]
    [Required]
    public string CurrentPassword { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "New Password")]
    [Required]
    public string NewPassword { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare(nameof(NewPassword))]
    [Required]
    public string ConfirmPassword { get; set; } = string.Empty;
}
