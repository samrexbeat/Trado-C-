using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace tradoAPI.Models
{
    public class Products
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        public int Price { get; set; }
        
        public decimal PointValue { get; set; }

        public int Status { get; set; }

        public DateTime Created { get; set; }


        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [NotMapped]
        public string ImagePath { get; set; }
    }
}
