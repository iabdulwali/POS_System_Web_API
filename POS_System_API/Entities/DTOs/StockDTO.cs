using POS_System_API.Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS_System_API.Entities.DTOs
{
    public class StockDTO
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; } = 0;
        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }
        public ProductDTO? Product { get; set; }
    }
}
