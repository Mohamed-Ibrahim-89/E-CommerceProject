using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Entities.Models
{
    internal class Order
    {
        public int Orderid { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CreateDat { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public Paymant? UserId { get; set; }
    }
}
