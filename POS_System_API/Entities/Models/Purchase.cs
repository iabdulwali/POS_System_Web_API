using System.ComponentModel.DataAnnotations;

namespace POS_System_API.Entities.Models
{
    public class Purchase
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int InvoiceNumber { get; set; }
        public decimal GrossTotal { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public decimal SalesTax { get; set; } = 0;
        public decimal OtherCharges { get; set; } = 0;
        public decimal NetTotal { get; set; } = 0;
        public Boolean IsReturn { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public ICollection<PurchaseProduct> PurchaseProducts { get; set; } = [];
    }
}
