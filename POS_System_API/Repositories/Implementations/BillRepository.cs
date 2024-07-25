using POS_System_API.Data;
using POS_System_API.Entities.DTOs;
using POS_System_API.Entities.Mappings;
using POS_System_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using POS_System_API.Entities.Models;

namespace POS_System_API.Repositories.Implementations
{
    public class BillRepository : IBillRepository
    {
        private readonly ApplicationDbContext _db;

        public BillRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<BillDTO>> getAll()
        {
            var bills = await _db.Bills.Include(bill => bill.BillProducts).ThenInclude(billProduct => billProduct.Product).ToListAsync();
            return bills.ConvertToBillDTO();
        }

        public async Task<BillDTO?> getById(Guid id)
        {
            var bill = await _db.Bills.Include(bill => bill.BillProducts).ThenInclude(billProduct => billProduct.Product).SingleOrDefaultAsync(bill => bill.Id == id);

            if (bill == null)
            {
                return null;
            }

            return bill.ConvertToBillDTO();
        }

        public async Task<BillDTO?> delete(Guid id)
        {
            var bill = await _db.Bills.Include(bill => bill.BillProducts).ThenInclude(billProduct => billProduct.Product).SingleOrDefaultAsync(bill => bill.Id == id);

            if (bill == null || bill.BillProducts == null)
            {
                return null;
            }

            _db.BillProducts.RemoveRange(bill.BillProducts);
            _db.Bills.Remove(bill);
            await _db.SaveChangesAsync();

            return bill.ConvertToBillDTO();
        }

        public async Task<BillDTO?> create(CreateBillDTO createBillDTO)
        {
            Bill bill = new Bill
            {
                CustomerName = createBillDTO.CustomerName,
                Discount = createBillDTO.Discount,
                SalesTax = createBillDTO.SalesTax,
                IsReturn = createBillDTO.IsReturn,
            };

            if (createBillDTO == null || createBillDTO.BillProducts == null)
            {
                return null;
            }

            var productIds = createBillDTO.BillProducts.Select(cp => cp.ProductId).ToList();

            var products = await _db.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            var productIdToPriceMap = products.ToDictionary(p => p.Id, p => p.RetailPrice);

            var stocks = await _db.Stocks
                .Where(s => productIds.Contains(s.ProductId))
                .ToListAsync();
            var newStocks = new List<Stock>();

            var productIdToStockMap = stocks.ToDictionary(s => s.ProductId);

            List<BillProduct> billProducts = new List<BillProduct>();

            foreach (var createBillProductDTO in createBillDTO.BillProducts)
            {
                if (productIdToPriceMap.TryGetValue(createBillProductDTO.ProductId, out var productPrice))
                {
                    decimal netTotal = (productPrice * createBillProductDTO.Quantity) - (productPrice * createBillProductDTO.Quantity * createBillProductDTO.Discount);
                    var billProduct = new BillProduct
                    {
                        BillId = bill.Id,
                        Quantity = createBillProductDTO.Quantity,
                        Discount = createBillProductDTO.Discount,
                        ProductId = createBillProductDTO.ProductId,
                        NetTotal = netTotal
                    };
                    bill.GrossTotal += netTotal;

                    if (bill.IsReturn)
                    {
                        if (productIdToStockMap.TryGetValue(createBillProductDTO.ProductId, out var stock))
                        {
                            stock.Quantity += createBillProductDTO.Quantity;
                            stock.UpdatedDate = DateTime.Now;
                        }
                        else
                        {
                            newStocks.Add(new Stock
                            {
                                ProductId = createBillProductDTO.ProductId,
                                Quantity = createBillProductDTO.Quantity,
                            });
                        }
                    }
                    else
                    {
                        if (productIdToStockMap.TryGetValue(createBillProductDTO.ProductId, out var stock))
                        {
                            stock.Quantity -= createBillProductDTO.Quantity;
                            if (stock.Quantity < 0)
                            {
                                throw new InvalidOperationException($"Stock quantity cannot be negative for product with ID {createBillProductDTO.ProductId}.");
                            }
                            stock.UpdatedDate = DateTime.Now;
                        }
                        else
                        {
                            throw new ArgumentException($"Stock information not found for product with ID {createBillProductDTO.ProductId}.");
                        }
                    }

                    billProducts.Add(billProduct);
                }
                else
                {
                    throw new ArgumentException($"Product with ID {createBillProductDTO.ProductId} not found.");
                }
            }

            bill.NetTotal = bill.GrossTotal + (bill.GrossTotal * bill.SalesTax) - (bill.GrossTotal * bill.Discount);

            _db.Bills.Add(bill);
            _db.BillProducts.AddRange(billProducts);
            _db.Stocks.UpdateRange(stocks);

            if (newStocks.Count > 0)
            {
                _db.Stocks.AddRange(newStocks);
            }

            _db.SaveChanges();

            var newBill = await _db.Bills.Include(p => p.BillProducts).ThenInclude(bp => bp.Product).SingleOrDefaultAsync(b => b.Id == bill.Id);

            if (newBill == null)
            {
                return null;
            }

            return newBill.ConvertToBillDTO();
        }
    }
}
