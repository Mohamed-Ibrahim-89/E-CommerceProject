using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceProject.Entities.Models
{
    public class Discount
    {
        public int DiscountId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal Percentage { get; set; }

        public bool Active { get; set; }

        [Display (Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
