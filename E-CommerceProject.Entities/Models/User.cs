namespace E_CommerceProject.Entities.Models;

public class User : IdentityUser
{
    public string? FullName { get; set; }
    public bool? IsDeleted { get; set; }
}
