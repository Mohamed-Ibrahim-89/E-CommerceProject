using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Entities.Models
{
    public class Cart
    {
        public int CartId { get; set; }

        public int CustomerId { get; set; }

        public int OrderId { get; set; }

        public int amount { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? DeletedAt { get; set;} 
    }
}
