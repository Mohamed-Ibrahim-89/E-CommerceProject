using System.ComponentModel.DataAnnotations;

namespace E_CommerceProject.Entities.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [MaxLength(500)]
        public string? Avatar { get; set; }
        [MaxLength(40)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(40)]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
