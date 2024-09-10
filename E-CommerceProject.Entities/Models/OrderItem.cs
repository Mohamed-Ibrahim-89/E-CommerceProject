using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Entities.Models
{
    internal class OrderItem
    {
        public int OrderItemId { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public int OrdeId { get; set; }
        public Order? OrderId { get; set; }
        public int ProductId { get; set; }
        public Product? Productid { get; set; }
    }
}
