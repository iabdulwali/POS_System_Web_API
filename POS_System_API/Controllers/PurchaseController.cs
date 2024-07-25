using Microsoft.AspNetCore.Mvc;
using POS_System_API.Entities.DTOs;
using POS_System_API.Repositories.Interfaces;

namespace POS_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseController(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PurchaseDTO>>> GetAll()
        {
            var purchases = await _purchaseRepository.getAll();
            return Ok(purchases);
        }

        [HttpGet("{id:Guid}", Name = "GetPurchaseById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PurchaseDTO>> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var purchaseDTO = await _purchaseRepository.getById(id);

            if (purchaseDTO == null)
            {
                return NotFound();
            }
            return Ok(purchaseDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PurchaseDTO>> Create([FromBody] CreatePurchaseDTO createPurchaseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (createPurchaseDTO == null)
            {
                return BadRequest();
            }

            try
            {
                var purchaseDTO = await _purchaseRepository.create(createPurchaseDTO);

                return Ok(purchaseDTO);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:Guid}", Name = "DeletePurchase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PurchaseDTO>> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var purchaseDTO = await _purchaseRepository.delete(id);

            if (purchaseDTO == null)
            {
                return NotFound();
            }

            return Ok(purchaseDTO);
        }
    }
}
