using POS_System_API.Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace POS_System_API.Entities.DTOs
{
    public class BillDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int InvoiceNumber { get; set; }
        public String CustomerName { get; set; } = String.Empty;
        public decimal GrossTotal { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public decimal SalesTax { get; set; } = 0;
        public decimal NetTotal { get; set; } = 0;
        public Boolean IsReturn { get; set; } = false;
        public ICollection<BillProductDTO?>? BillProducts { get; set; }
    }
}
