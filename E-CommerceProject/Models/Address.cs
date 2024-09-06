using System.ComponentModel.DataAnnotations;

namespace E_CommerceProject.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        [MaxLength(30)]
        public string PhoneNumber { get; set; } = string.Empty;
        [MaxLength(200)]
        public string AddressLine1 { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? AddressLine2 { get; set; }
        [MaxLength(50)]
        public string Country { get; set; } = string.Empty;
        [MaxLength(50)]
        public string City { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Landmark { get; set; }
        public string? ZipCode { get; set; }
        [MaxLength(50)]
        public string? State { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
