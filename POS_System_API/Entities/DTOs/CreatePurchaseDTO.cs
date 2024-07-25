using POS_System_API.Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace POS_System_API.Entities.DTOs
{
    public class CreatePurchaseDTO
    {
        [Required]
        [Range(0, 0.99, ErrorMessage = "Range should be between 0 and 0.99")]
        public decimal Discount { get; set; } = 0;
        [Required]
        [Range(0, 0.99, ErrorMessage = "Range should be between 0 and 0.99")]
        public decimal SalesTax { get; set; } = 0;
        [Required]
        [Range(0, 100000, ErrorMessage = "Range should be between 0 and 100000")]
        public decimal OtherCharges { get; set; } = 0;
        [Required]
        public Boolean IsReturn { get; set; } = false;
        [Required]
        [MinLength(1, ErrorMessage = "There should atleast be one purchase product.")]
        public List<CreatePurchaseProductDTO>? PurchaseProducts { get; set; }
    }
}
