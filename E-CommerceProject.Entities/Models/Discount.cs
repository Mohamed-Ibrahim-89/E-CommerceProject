using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Entities.Models
{
    public class Discount
    {
        public int DiscountId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        [MaxLength(200)]
        public int  DiscountPercent { get; set; }
        [MaxLength(50)]
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set;}
        public DateTime? DeletedAt { get; set; }
    }
}
