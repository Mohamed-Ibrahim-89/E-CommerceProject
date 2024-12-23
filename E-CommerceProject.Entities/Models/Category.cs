﻿using System.ComponentModel.DataAnnotations;

namespace E_CommerceProject.Entities.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
