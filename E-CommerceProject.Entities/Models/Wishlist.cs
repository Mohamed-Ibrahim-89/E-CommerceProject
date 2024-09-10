using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace E_CommerceProject.Entities.Models
{
    public class Wishlist
    {

        public int WishlistId { get; set; }  

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? DeletedAt { get; set; }

    }
}
