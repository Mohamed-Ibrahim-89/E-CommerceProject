namespace E_CommerceProject.Entities.Models;

public class User : IdentityUser
{
    [MaxLength(40)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(40)]
    public string LastName { get; set; } = string.Empty;

    public string? FullName { get; set; }

    public bool? IsDeleted { get; set; }
}
