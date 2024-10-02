using System.ComponentModel.DataAnnotations;

namespace E_CommerceProject.Entities.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime DeleatedAt { get; set; } = DateTime.Now;
    }
}
