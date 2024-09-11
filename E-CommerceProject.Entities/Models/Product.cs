using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Entities.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(700)]
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Cover { get; set; } = string.Empty;
        public int QuantityInStock { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime DeleatedAt { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int DiscountId { get; set; }

        public Discount? Discount { get; set; }

    }
}
