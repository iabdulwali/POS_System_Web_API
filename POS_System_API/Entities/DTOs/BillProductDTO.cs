using POS_System_API.Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS_System_API.Entities.DTOs
{
    public class BillProductDTO
    {
        public int Quantity { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public decimal NetTotal { get; set; } = 0;
        public Guid ProductId { get; set; }
        public ProductDTO? Product { get; set; }
    }
}
