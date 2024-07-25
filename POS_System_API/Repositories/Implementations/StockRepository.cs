using Microsoft.AspNetCore.JsonPatch;
using POS_System_API.Data;
using POS_System_API.Entities.DTOs;
using POS_System_API.Repositories.Interfaces;
using POS_System_API.Entities.Mappings;
using Microsoft.EntityFrameworkCore;

namespace POS_System_API.Repositories.Implementations
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _db;

        public StockRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<StockDTO>> getAll()
        {
            var stocks = await _db.Stocks.Include(stock => stock.Product).ToListAsync();
            return stocks.ConvertToStockDTO();
        }

        public async Task<StockDTO?> getById(Guid id)
        {
            var stock = await _db.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

            if (stock == null)
            {
                return null;
            }

            _db.Entry(stock).Reference(product => product.Product).Load();

            return stock.ConvertToStockDTO();
        }

        public async Task<StockDTO?> create(CreateStockDTO createStockDTO)
        {
            var newStock = createStockDTO.ToStock();
            await _db.Stocks.AddAsync(newStock);
            await _db.SaveChangesAsync();
            var stock = await _db.Stocks.Include(s => s.Product).FirstOrDefaultAsync(stock => stock.Id == newStock.Id);

            if (stock == null)
            {
                return null;
            }

            return stock.ConvertToStockDTO();
        }

        public async Task<StockDTO?> update(Guid id, CreateStockDTO createStockDTO)
        {
            var stock = await _db.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

            if (stock == null)
            {
                return null;
            }

            createStockDTO.ToStock(stock);

            await _db.SaveChangesAsync();

            _db.Entry(stock).Reference(product => product.Product).Load();

            return stock.ConvertToStockDTO();
        }

        public async Task<StockDTO?> updatePartial(Guid id, JsonPatchDocument<StockDTO> patchDTO)
        {
            var stock = await _db.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

            if (stock == null)
            {
                return null;
            }
            _db.Entry(stock).Reference(product => product.Product).Load();
            var stockDTO = stock.ConvertToStockDTO();
            patchDTO.ApplyTo(stockDTO);
            stockDTO.ToStock(stock);
            await _db.SaveChangesAsync();

            return stockDTO;
        }

        public async Task<StockDTO?> delete(Guid id)
        {
            var stock = await _db.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

            if (stock == null)
            {
                return null;
            }

            _db.Stocks.Remove(stock);
            await _db.SaveChangesAsync();

            _db.Entry(stock).Reference(product => product.Product).Load();

            return stock.ConvertToStockDTO();
        }
    }
}
