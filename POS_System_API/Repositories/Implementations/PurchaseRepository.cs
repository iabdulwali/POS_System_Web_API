using POS_System_API.Data;
using POS_System_API.Entities.DTOs;
using POS_System_API.Entities.Mappings;
using POS_System_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using POS_System_API.Entities.Models;

namespace POS_System_API.Repositories.Implementations
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _db;

        public PurchaseRepository(ApplicationDbContext db) 
        {
            _db = db;
        }

        public async Task<List<PurchaseDTO>> getAll()
        {
            var purchases = await _db.Purchases.Include(purchase => purchase.PurchaseProducts).ThenInclude(purchaseProduct => purchaseProduct.Product).ToListAsync();
            return purchases.ConvertToPurchaseDTO();
        }

        public async Task<PurchaseDTO?> getById(Guid id)
        {
            var purchase = await _db.Purchases.Include(purchase => purchase.PurchaseProducts).ThenInclude(purchaseProduct => purchaseProduct.Product).SingleOrDefaultAsync(purchase => purchase.Id == id);

            if (purchase == null)
            {
                return null;
            }

            return purchase.ConvertToPurchaseDTO();
        }

        public async Task<PurchaseDTO?> delete(Guid id)
        {
            var purchase = await _db.Purchases.Include(purchase => purchase.PurchaseProducts).ThenInclude(purchaseProduct => purchaseProduct.Product).SingleOrDefaultAsync(purchase => purchase.Id == id);

            if (purchase == null || purchase.PurchaseProducts == null)
            {
                return null;
            }

            _db.PurchaseProducts.RemoveRange(purchase.PurchaseProducts);
            _db.Purchases.Remove(purchase);
            await _db.SaveChangesAsync();

            return purchase.ConvertToPurchaseDTO();
        }

        public async Task<PurchaseDTO?> create(CreatePurchaseDTO createPurchaseDTO)
        {
            Purchase purchase = new Purchase
            {
                Discount = createPurchaseDTO.Discount,
                SalesTax = createPurchaseDTO.SalesTax,
                OtherCharges = createPurchaseDTO.OtherCharges,
                IsReturn = createPurchaseDTO.IsReturn,
            };

            if (createPurchaseDTO == null || createPurchaseDTO.PurchaseProducts == null)
            {
                return null;
            }

            var productIds = createPurchaseDTO.PurchaseProducts.Select(cp => cp.ProductId).ToList();

            var products = await _db.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            var productIdToPriceMap = products.ToDictionary(p => p.Id, p => p.PurchasePrice);

            var stocks = await _db.Stocks
                .Where(s => productIds.Contains(s.ProductId))
                .ToListAsync();
            var newStocks = new List<Stock>();

            var productIdToStockMap = stocks.ToDictionary(s => s.ProductId);

            List<PurchaseProduct> purchaseProducts = new List<PurchaseProduct>();
            
            foreach (var createPurchaseProductDTO in createPurchaseDTO.PurchaseProducts)
            {
                if (productIdToPriceMap.TryGetValue(createPurchaseProductDTO.ProductId, out var productPrice))
                {
                    decimal netTotal = (productPrice * createPurchaseProductDTO.Quantity) - (productPrice * createPurchaseProductDTO.Quantity * createPurchaseProductDTO.Discount);
                    var purchaseProduct = new PurchaseProduct
                    {
                        PurchaseId = purchase.Id,
                        Quantity = createPurchaseProductDTO.Quantity,
                        Discount = createPurchaseProductDTO.Discount,
                        ProductId = createPurchaseProductDTO.ProductId,
                        NetTotal = netTotal
                    };
                    purchase.GrossTotal += netTotal;

                    if (!purchase.IsReturn)
                    {
                        if (productIdToStockMap.TryGetValue(createPurchaseProductDTO.ProductId, out var stock))
                        {
                            stock.Quantity += createPurchaseProductDTO.Quantity;
                            stock.UpdatedDate = DateTime.Now;
                        }
                        else
                        {
                            newStocks.Add(new Stock
                            {
                                ProductId = createPurchaseProductDTO.ProductId,
                                Quantity = createPurchaseProductDTO.Quantity,
                            });
                        }
                    }
                    else
                    {
                        if (productIdToStockMap.TryGetValue(createPurchaseProductDTO.ProductId, out var stock))
                        {
                            stock.Quantity -= createPurchaseProductDTO.Quantity;
                            if (stock.Quantity < 0)
                            {
                                throw new InvalidOperationException($"Stock quantity cannot be negative for product with ID {createPurchaseProductDTO.ProductId}.");
                            }
                            stock.UpdatedDate = DateTime.Now;
                        }
                        else
                        {
                            throw new ArgumentException($"Stock information not found for product with ID {createPurchaseProductDTO.ProductId}.");
                        }
                    }

                    purchaseProducts.Add(purchaseProduct);
                }
                else
                {
                    throw new ArgumentException($"Product with ID {createPurchaseProductDTO.ProductId} not found.");
                }
            }

            purchase.NetTotal = (purchase.GrossTotal + (purchase.GrossTotal * purchase.SalesTax) - (purchase.GrossTotal * purchase.Discount)) + purchase.OtherCharges;

            _db.Purchases.Add(purchase);
            _db.PurchaseProducts.AddRange(purchaseProducts);
            _db.Stocks.UpdateRange(stocks);

            if (newStocks.Count > 0) 
            { 
                _db.Stocks.AddRange(newStocks);
            }

            _db.SaveChanges();

            var purch = await _db.Purchases.Include(p => p.PurchaseProducts).ThenInclude(pp => pp.Product).SingleOrDefaultAsync(p => p.Id == purchase.Id);

            if (purch == null)
            {
                return null;
            }

            return purch.ConvertToPurchaseDTO(); 
        }

    }
}
