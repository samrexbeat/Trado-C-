using System.ComponentModel.DataAnnotations;

namespace tradoAPI.Models
{
    public class Carts
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        
        public int userId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }

        public int Price { get; set; }
        [Required]

        public decimal TotalPrice { get; set; }
        
        public DateTime Created { get; set; }
    }
}
