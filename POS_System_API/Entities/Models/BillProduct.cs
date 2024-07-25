using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System_API.Entities.Models
{
    public class BillProduct
    {
        public int Quantity { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public decimal NetTotal { get; set; } = 0;
        public Guid BillId { get; set; }
        [ForeignKey("BillId")]
        public Bill? Bill { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
