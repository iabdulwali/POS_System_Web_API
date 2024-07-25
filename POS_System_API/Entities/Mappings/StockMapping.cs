using POS_System_API.Entities.DTOs;
using POS_System_API.Entities.Models;

namespace POS_System_API.Entities.Mappings
{
    public static class StockMapping
    {
        public static StockDTO ConvertToStockDTO(this Stock stock)
        {
            return new StockDTO
            {
                Id = stock.Id,
                Quantity = stock.Quantity,
                ProductId = stock.ProductId,
                Product = stock.Product != null ? new ProductDTO
                {
                    Id = stock.Product.Id,
                    Name = stock.Product.Name,
                    PackSize = stock.Product.PackSize,
                    PurchasePrice = stock.Product.PurchasePrice,
                    RetailPrice = stock.Product.RetailPrice
                } : null,
            };
        }

        public static List<StockDTO> ConvertToStockDTO(this List<Stock> stocks)
        {
            return stocks.Select(stock => stock.ConvertToStockDTO()).ToList();
        }

        public static Stock ToStock(this CreateStockDTO createStockDTO)
        {
            return new Stock
            {
                ProductId = createStockDTO.ProductId,
                Quantity = createStockDTO.Quantity
            };
        }

        public static void ToStock(this CreateStockDTO createStockDTO, Stock stock)
        {
            stock.ProductId = createStockDTO.ProductId;
            stock.Quantity = createStockDTO.Quantity;
            stock.UpdatedDate = DateTime.Now;
        }

        public static void ToStock(this StockDTO stockDTO, Stock stock)
        {
            stock.ProductId = stockDTO.ProductId;
            stock.Quantity = stockDTO.Quantity;
            stock.UpdatedDate = DateTime.Now;
        }
    }
}
