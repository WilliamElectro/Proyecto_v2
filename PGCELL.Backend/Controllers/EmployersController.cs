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
    public class EmployersController : GenericController<Employer>
    {
        private readonly IEmployerUnitOfWork _employerUnitOfWork;

        public EmployersController(IGenericUnitOfWork<Employer> unit, IEmployerUnitOfWork employerUnitOfWork) : base(unit)
        {
            _employerUnitOfWork = employerUnitOfWork;
        }

        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<IActionResult> GetComboAsync()
        {
            IEnumerable<Employer> combo = await _employerUnitOfWork.GetComboAsync();
            return Ok(combo);
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _employerUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _employerUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}
