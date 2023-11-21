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
    public class TypeNoveltiesController : GenericController<TypeNovelty>
    {
        private readonly ITypeNoveltiesUnitOfWork _typeNoveltyUnitOfWork;

        public TypeNoveltiesController(IGenericUnitOfWork<TypeNovelty> unit, ITypeNoveltiesUnitOfWork typeNoveltyUnitOfWork) : base(unit)
        {
            _typeNoveltyUnitOfWork = typeNoveltyUnitOfWork;
        }

        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<IActionResult> GetComboAsync()
        {
            return Ok(await _typeNoveltyUnitOfWork.GetComboAsync());
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _typeNoveltyUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _typeNoveltyUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}
