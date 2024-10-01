using System.ComponentModel.DataAnnotations;

namespace E_CommerceProject.Entities.ViewModels
{
    public class LoginViewModel
    {
        [Required, StringLength(256)]
        public string Username { get; set; } = string.Empty;

        [Required, StringLength(256), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; } = false;
    }
}
