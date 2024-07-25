using Microsoft.AspNetCore.JsonPatch;
using POS_System_API.Entities.DTOs;

namespace POS_System_API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<ProductDTO>> getAll();
        public Task<ProductDTO?> getById(Guid id);
        public Task<ProductDTO?> create(ProductDTO productDTO);
        public Task<ProductDTO?> update(Guid id, ProductDTO productDTO);
        public Task<ProductDTO?> updatePartial(Guid id, JsonPatchDocument<ProductDTO> patchDTO);
        public Task<ProductDTO?> delete(Guid id);
        public Task<bool> isExists(Guid id);
    }
}
