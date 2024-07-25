using POS_System_API.Entities.DTOs;
using POS_System_API.Entities.Models;

namespace POS_System_API.Entities.Mappings
{
    public static class PurchaseProductMapping
    {
        public static PurchaseProductDTO ConvertToPurchaseProductDTO(this PurchaseProduct purchaseProduct)
        {
            return new PurchaseProductDTO
            {
                Quantity = purchaseProduct.Quantity,
                Discount = purchaseProduct.Discount,
                NetTotal = purchaseProduct.NetTotal,
                ProductId = purchaseProduct.ProductId,
                Product = purchaseProduct.Product != null ? new ProductDTO
                {
                    Id = purchaseProduct.Product.Id,
                    Name = purchaseProduct.Product.Name,
                    PackSize = purchaseProduct.Product.PackSize,
                    PurchasePrice = purchaseProduct.Product.PurchasePrice,
                    RetailPrice = purchaseProduct.Product.RetailPrice
                } : null,
            };
        }

        public static List<PurchaseProductDTO> ConvertToPurchaseProductDTO(this ICollection<PurchaseProduct> purchaseProducts)
        {
            return purchaseProducts.Select(purchaseProduct => purchaseProduct.ConvertToPurchaseProductDTO()).ToList();
        }
    }
}
