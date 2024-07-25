using System.ComponentModel.DataAnnotations;

namespace POS_System_API.Entities.DTOs
{
    public class CreatePurchaseProductDTO
    {
        [Required]
        public int Quantity { get; set; } = 0;
        [Required]
        [Range(0, 0.99, ErrorMessage = "Range should be between 0 and 0.99")]
        public decimal Discount { get; set; } = 0;
        [Required]
        public Guid ProductId { get; set; }
    }
}
