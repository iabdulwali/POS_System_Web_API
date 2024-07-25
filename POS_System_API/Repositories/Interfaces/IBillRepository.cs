using POS_System_API.Entities.DTOs;

namespace POS_System_API.Repositories.Interfaces
{
    public interface IBillRepository
    {
        public Task<List<BillDTO>> getAll();
        public Task<BillDTO?> getById(Guid id);
        public Task<BillDTO?> create(CreateBillDTO createBillDTO);
        //public Task<BillDTO?> update(Guid id, BillDTO billDTO);
        public Task<BillDTO?> delete(Guid id);
    }
}
