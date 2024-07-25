using System.ComponentModel.DataAnnotations;

namespace POS_System_API.Entities.DTOs
{
    public class CreateBillDTO
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Name should be at max 30 characters.")]
        [MinLength(10, ErrorMessage = "Name should be at min 10 characters.")]
        public String CustomerName { get; set; } = String.Empty;
        [Required]
        [Range(0, 0.99, ErrorMessage = "Range should be between 0 and 0.99")]
        public decimal Discount { get; set; } = 0;
        [Required]
        [Range(0, 0.99, ErrorMessage = "Range should be between 0 and 0.99")]
        public decimal SalesTax { get; set; } = 0;
        [Required]
        public Boolean IsReturn { get; set; } = false;
        [Required]
        [MinLength(1, ErrorMessage = "There should atleast be one bill product.")]
        public List<CreateBillProductDTO>? BillProducts { get; set; }
    }
}
