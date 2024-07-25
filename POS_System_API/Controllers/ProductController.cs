using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using POS_System_API.Entities.DTOs;
using POS_System_API.Repositories.Interfaces;

namespace POS_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProductDTO>>> GetAll()
        {
            var products = await _productRepository.getAll();
            return Ok(products);
        }

        [HttpGet("{id:Guid}", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var productDTO = await _productRepository.getById(id);

            if (productDTO == null)
            {
                return NotFound();
            }
            return Ok(productDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductDTO>> Create([FromBody] ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productDTO == null)
            {
                return BadRequest();
            }

            var newProductDTO = await _productRepository.create(productDTO);

            return Ok(newProductDTO);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var productDTO = await _productRepository.delete(id);

            if (productDTO == null)
            {
                return NotFound();
            }

            return Ok(productDTO);
        }

        [HttpPut("{id:Guid}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> Update([FromRoute] Guid id, [FromBody] ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id == Guid.Empty || productDTO == null)
            {
                return BadRequest();
            }

            var updatedProductDTO = await _productRepository.update(id, productDTO);

            if (updatedProductDTO == null)
            {
                return NotFound();
            }

            return Ok(updatedProductDTO);
        }

        [HttpPatch("{id:Guid}", Name = "UpdatePartialProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> UpdatePartial(Guid id, JsonPatchDocument<ProductDTO> patchDTO)
        {
            if (patchDTO == null || id == Guid.Empty)
            {
                return BadRequest();
            }

            var productDTO = await _productRepository.updatePartial(id, patchDTO);

            if (productDTO == null)
            {
                return NotFound();
            }

            return Ok(productDTO);
        }
    }
}