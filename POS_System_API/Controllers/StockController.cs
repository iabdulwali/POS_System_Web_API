using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using POS_System_API.Entities.DTOs;
using POS_System_API.Repositories.Interfaces;

namespace POS_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        private readonly IProductRepository _productRepository;

        public StockController(IStockRepository stockRepository, IProductRepository productRepository)
        {
            _stockRepository = stockRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<StockDTO>>> GetAll()
        {
            var stocks = await _stockRepository.getAll();
            return Ok(stocks);
        }

        [HttpGet("{id:Guid}", Name = "GetStockById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StockDTO>> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var stockDTO = await _stockRepository.getById(id);

            if (stockDTO == null)
            {
                return NotFound();
            }
            return Ok(stockDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StockDTO>> Create([FromBody] CreateStockDTO createStockDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (createStockDTO == null)
            {
                return BadRequest();
            }

            if(!await _productRepository.isExists(createStockDTO.ProductId))
            {
                return BadRequest("Product with the given ID does not exist.");
            }

            var stockDTO = await _stockRepository.create(createStockDTO);

            return Ok(stockDTO);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteStock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StockDTO>> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var stockDTO = await _stockRepository.delete(id);

            if (stockDTO == null)
            {
                return NotFound();
            }

            return Ok(stockDTO);
        }

        [HttpPut("{id:Guid}", Name = "UpdateStock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StockDTO>> Update([FromRoute] Guid id, [FromBody] CreateStockDTO createStockDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id == Guid.Empty || createStockDTO == null)
            {
                return BadRequest();
            }

            if (!await _productRepository.isExists(createStockDTO.ProductId))
            {
                return BadRequest("Product with the given ID does not exist.");
            }

            var updatedStockDTO = await _stockRepository.update(id, createStockDTO);

            if (updatedStockDTO == null)
            {
                return NotFound();
            }

            return Ok(updatedStockDTO);
        }

        [HttpPatch("{id:Guid}", Name = "UpdatePartialStock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StockDTO>> UpdatePartial(Guid id, JsonPatchDocument<StockDTO> patchDTO)
        {
            if (patchDTO == null || id == Guid.Empty)
            {
                return BadRequest();
            }

            var stockDTO = await _stockRepository.updatePartial(id, patchDTO);

            if (stockDTO == null)
            {
                return NotFound();
            }

            return Ok(stockDTO);
        }
    }
}
