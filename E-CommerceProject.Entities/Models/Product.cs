using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceProject.Entities.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(700)]
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        [MaxLength (150)]
        public string Cover { get; set; } = string.Empty;
        public int QuantityInStock { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int ? DiscountId { get; set; }
        public Discount? Discount { get; set; }
    }
}
