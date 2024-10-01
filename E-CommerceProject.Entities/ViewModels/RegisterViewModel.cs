using System.ComponentModel.DataAnnotations;

namespace E_CommerceProject.Entities.ViewModels
{
    public class RegisterViewModel
    {
        [Required, StringLength(256)]
        public string Username { get; set; } = string.Empty;

        [Required, StringLength(256), EmailAddress(ErrorMessage ="Email not valid")]
        public string Email { get; set; } = string.Empty;

        [Required,StringLength(256), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required, StringLength(256), DataType(DataType.Password), Display(Name ="Confirm password"), Compare("Password", ErrorMessage ="Does not Mathch")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
