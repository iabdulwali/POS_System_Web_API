using POS_System_API.Entities.DTOs;

namespace POS_System_API.Repositories.Interfaces
{
    public interface IPurchaseRepository
    {
        public Task<List<PurchaseDTO>> getAll();
        public Task<PurchaseDTO?> getById(Guid id);
        public Task<PurchaseDTO?> create(CreatePurchaseDTO createPurchaseDTO);
        //public Task<PurchaseDTO?> update(Guid id, PurchaseDTO purchaseDTO);
        public Task<PurchaseDTO?> delete(Guid id);
    }
}
