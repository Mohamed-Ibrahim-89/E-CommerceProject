﻿using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceProject.Entities.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
