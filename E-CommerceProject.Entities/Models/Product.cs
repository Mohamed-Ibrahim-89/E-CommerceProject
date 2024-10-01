using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceProject.Entities.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(1500)]
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(8,2)")]
        [Range(1, 1000000, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        [MaxLength (150)]
        public string Cover { get; set; } = string.Empty;
        [Display(Name = "Quantity")]
        [Range(1, 10000, ErrorMessage = "Price must be greater than 0")]
        public int QuantityInStock { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Display(Name = "Discount")]
        public int? DiscountId { get; set; }
        public Discount? Discount { get; set; }

    }
}
