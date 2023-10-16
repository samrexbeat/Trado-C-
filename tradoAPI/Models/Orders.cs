using System.ComponentModel.DataAnnotations;

namespace tradoAPI.Models
{
    public class Orders
    {
        [Key]
        public Guid id { get; set; }
        [Required]

        public int userId { get; set; }
        [Required]
        
        public string  orderNo { get; set; }

        public decimal totalOrder { get; set; }

        public DateTime Created { get; set; }
    }
}
