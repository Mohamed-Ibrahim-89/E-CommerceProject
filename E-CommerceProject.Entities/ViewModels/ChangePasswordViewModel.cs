using System.ComponentModel.DataAnnotations;

namespace E_CommerceProject.Entities.ViewModels
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Cusrrent Password")]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
