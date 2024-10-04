using System.ComponentModel.DataAnnotations;

namespace E_CommerceProject.Entities.ViewModels
{
    public class AccountViewModel
    {
        [Required, StringLength(256)]
        public string Username { get; set; } = string.Empty;

        [Required, StringLength(256), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; } = false;


        [Required, StringLength(256), EmailAddress(ErrorMessage = "Email not valid")]
        public string Email { get; set; } = string.Empty;

    }
}
