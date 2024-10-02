using System.ComponentModel.DataAnnotations;

namespace E_CommerceProject.Entities.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; } = string.Empty;
        [Display(Name = "Role")]
        public string RoleName { get; set; } = string.Empty;
    }
}
