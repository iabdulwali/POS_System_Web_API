using POS_System_API.Entities.DTOs;
using POS_System_API.Entities.Models;

namespace POS_System_API.Entities.Mappings
{
    public static class ProductMapping
    {
        public static ProductDTO ConvertToProductDTO(this Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                PackSize = product.PackSize,
                PurchasePrice = product.PurchasePrice,
                RetailPrice = product.RetailPrice
            };
        }

        public static List<ProductDTO> ConvertToProductDTO(this List<Product> products)
        {
            return products.Select(product => product.ConvertToProductDTO()).ToList();
        }

        public static Product ToProduct(this ProductDTO productDTO)
        {
            return new Product
            {
                Name = productDTO.Name,
                PackSize = productDTO.PackSize,
                PurchasePrice = productDTO.PurchasePrice,
                RetailPrice = productDTO.RetailPrice
            };
        }

        public static void ToProduct(this ProductDTO productDTO, Product product)
        {
            product.Name = productDTO.Name;
            product.PackSize = productDTO.PackSize;
            product.PurchasePrice = productDTO.PurchasePrice;
            product.RetailPrice = productDTO.RetailPrice;
            product.UpdatedDate = DateTime.Now;
        }
    }
}
