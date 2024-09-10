using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;

namespace E_CommerceProject.Entities.Models
{
    public class Wishlist
    {

        public int WishlistId { get; set; }
        [MaxLength(50)]
        public int CustomerId { get; set; }
        [MaxLength(50)]

        public Customer Customer { get; set; }
        public int ProductId { get; set; }
        [MaxLength (50)]

        public Product Product { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }

    }
}
