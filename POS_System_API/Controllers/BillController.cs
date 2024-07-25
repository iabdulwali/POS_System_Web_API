using Microsoft.AspNetCore.Mvc;
using POS_System_API.Entities.DTOs;
using POS_System_API.Repositories.Interfaces;

namespace POS_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillRepository _billRepository;

        public BillController(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<BillDTO>>> GetAll()
        {
            var bills = await _billRepository.getAll();
            return Ok(bills);
        }

        [HttpGet("{id:Guid}", Name = "GetBillById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BillDTO>> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var billDTO = await _billRepository.getById(id);

            if (billDTO == null)
            {
                return NotFound();
            }
            return Ok(billDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BillDTO>> Create([FromBody] CreateBillDTO createBillDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (createBillDTO == null)
            {
                return BadRequest();
            }

            try
            {
                var billDTO = await _billRepository.create(createBillDTO);

                return Ok(billDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:Guid}", Name = "DeleteBill")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BillDTO>> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var billDTO = await _billRepository.delete(id);

            if (billDTO == null)
            {
                return NotFound();
            }

            return Ok(billDTO);
        }
    }
}
