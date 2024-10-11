using System.ComponentModel.DataAnnotations;

namespace E_CommerceProject.Entities.Models
{
    public class CustomerInfo
    {
        public int CustomerInfoId { get; set; }

        [MaxLength(40)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(40)]
        public string LastName { get; set; } = string.Empty;

        [Display(Name= "Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }

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


        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
