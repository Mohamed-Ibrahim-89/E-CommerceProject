namespace E_CommerceProject.Entities.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<RolesCheckedViewModel> Roles { get; set; } = default!;
    }
}
