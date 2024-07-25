using POS_System_API.Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace POS_System_API.Entities.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PackSize { get; set; } = 0;
        public decimal PurchasePrice { get; set; } = 0;
        public decimal RetailPrice { get; set; } = 0;
    }
}
