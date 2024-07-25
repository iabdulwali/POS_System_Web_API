using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS_System_API.Data;
using POS_System_API.Entities.DTOs;
using POS_System_API.Entities.Mappings;
using POS_System_API.Repositories.Interfaces;

namespace POS_System_API.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<ProductDTO>> getAll()
        {
            var products = await _db.Products.ToListAsync();
            return products.ConvertToProductDTO();
        }

        public async Task<ProductDTO?> getById(Guid id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(product => product.Id == id);

            if (product == null)
            {
                return null;
            }

            return product.ConvertToProductDTO();
        }

        public async Task<ProductDTO?> create(ProductDTO productDTO)
        {
            var newProduct = productDTO.ToProduct();
            await _db.Products.AddAsync(newProduct);
            await _db.SaveChangesAsync();
            var product = await _db.Products.FirstOrDefaultAsync(product => product.Id == newProduct.Id);

            if (product == null)
            {
                return null;
            }

            return product.ConvertToProductDTO();
        }

        public async Task<ProductDTO?> update(Guid id, ProductDTO productDTO)
        {
            var product = await _db.Products.FirstOrDefaultAsync(product => product.Id == id);

            if (product == null)
            {
                return null;
            }

            productDTO.ToProduct(product);

            await _db.SaveChangesAsync();

            return product.ConvertToProductDTO();
        }

        public async Task<ProductDTO?> updatePartial(Guid id, JsonPatchDocument<ProductDTO> patchDTO)
        {
            var product = await _db.Products.FirstOrDefaultAsync(product => product.Id == id);

            if (product == null)
            {
                return null;
            }

            var productDTO = product.ConvertToProductDTO();
            patchDTO.ApplyTo(productDTO);
            productDTO.ToProduct(product);
            await _db.SaveChangesAsync();

            return productDTO;
        }

        public async Task<ProductDTO?> delete(Guid id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(product => product.Id == id);

            if (product == null)
            {
                return null;
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return product.ConvertToProductDTO();
        }

        public async Task<bool> isExists(Guid id)
        {
            return await _db.Products.AnyAsync(product => product.Id == id);
        }
    }
}
