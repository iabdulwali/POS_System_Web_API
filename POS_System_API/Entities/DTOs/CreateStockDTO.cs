using POS_System_API.Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System_API.Entities.DTOs
{
    public class CreateStockDTO
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; } = 0;
    }
}
