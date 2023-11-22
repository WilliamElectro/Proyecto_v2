using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGCELL.Backend.UnitsOfWork;
using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;

namespace PGCELL.Backend.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ModalitiesController : GenericController<Modality>
    {
        private readonly IModalitiesUnitOfWork _modalitiesUnitOfWork;

        public ModalitiesController(IGenericUnitOfWork<Modality> unit, IModalitiesUnitOfWork modalitiesUnitOfWork) : base(unit)
        {
            _modalitiesUnitOfWork = modalitiesUnitOfWork;
        }

        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<IActionResult> GetComboAsync()
        {
            IEnumerable<Modality> combo = await _modalitiesUnitOfWork.GetComboAsync();
            return Ok(combo);
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _modalitiesUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _modalitiesUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

    }
}
