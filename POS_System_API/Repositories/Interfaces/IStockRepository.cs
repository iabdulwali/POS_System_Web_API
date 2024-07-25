using Microsoft.AspNetCore.JsonPatch;
using POS_System_API.Entities.DTOs;

namespace POS_System_API.Repositories.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<StockDTO>> getAll();
        public Task<StockDTO?> getById(Guid id);
        public Task<StockDTO?> create(CreateStockDTO createStockDTO);
        public Task<StockDTO?> update(Guid id, CreateStockDTO createStockDTO);
        public Task<StockDTO?> updatePartial(Guid id, JsonPatchDocument<StockDTO> patchDTO);
        public Task<StockDTO?> delete(Guid id);
    }
}
