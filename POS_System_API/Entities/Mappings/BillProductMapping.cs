using POS_System_API.Entities.DTOs;
using POS_System_API.Entities.Models;

namespace POS_System_API.Entities.Mappings
{
    public static class BillProductMapping
    {
        public static BillProductDTO ConvertToBillProductDTO(this BillProduct billProduct)
        {
            return new BillProductDTO
            {
                Quantity = billProduct.Quantity,
                Discount = billProduct.Discount,
                NetTotal = billProduct.NetTotal,
                ProductId = billProduct.ProductId,
                Product = billProduct.Product != null ? new ProductDTO
                {
                    Id = billProduct.Product.Id,
                    Name = billProduct.Product.Name,
                    PackSize = billProduct.Product.PackSize,
                    PurchasePrice = billProduct.Product.PurchasePrice,
                    RetailPrice = billProduct.Product.RetailPrice
                } : null,
            };
        }

        public static List<BillProductDTO> ConvertToBillProductDTO(this ICollection<BillProduct> billProducts)
        {
            return billProducts.Select(billProduct => billProduct.ConvertToBillProductDTO()).ToList();
        }
    }
}
