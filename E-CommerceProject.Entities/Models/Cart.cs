using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Entities.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        [MaxLength(50)]
        public int CustomerId { get; set; }
        [MaxLength(50)]
        public int OrderId { get; set; }
        [MaxLength(50)]
        public int amount { get; set; }
        [MaxLength (50)]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set;}

        public Customer Customer { get; set; } = default!;
        public Order Order { get; set; }
    }
}
