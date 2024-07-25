using System.ComponentModel.DataAnnotations;

namespace POS_System_API.Entities.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public int PackSize { get; set; } = 0;
        public decimal PurchasePrice { get; set; } = 0;
        public decimal RetailPrice { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public Stock? Stock { get; set; }
        public ICollection<BillProduct>? BillProducts { get; set; }
        public ICollection<PurchaseProduct>? PurchaseProducts { get; set; }
    }
}
