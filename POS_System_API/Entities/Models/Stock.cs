using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System_API.Entities.Models
{
    public class Stock
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Quantity { get; set; } = 0;
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
