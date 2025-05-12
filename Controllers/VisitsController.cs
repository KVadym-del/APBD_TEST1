using Microsoft.AspNetCore.Mvc;
using APBD_TEST1.Services;
using APBD_TEST1.Models.DTOs;

namespace APBD_TEST1.Controllers
{
    [ApiController]
    [Route("api/visits")]
    public class VisitsController : ControllerBase
    {
        private readonly IVisitService _service;

        public VisitsController(IVisitService service)
        {
            _service = service;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVisit(int id)
        {
            var visit = await _service.GetVisitDetailsAsync(id);
            if (visit == null)
                return NotFound(new { message = "Visit not found." });
            return Ok(visit);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddVisit([FromBody] VisitCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (success, error) = await _service.AddVisitAsync(dto);
            if (!success)
            {
                if (error!.Contains("already exists"))
                    return Conflict(new { message = error });
                if (error.Contains("does not exist"))
                    return NotFound(new { message = error });
                return BadRequest(new { message = error });
            }
            return CreatedAtAction(nameof(GetVisit), new { id = dto.VisitId }, null);
        }
    }
}