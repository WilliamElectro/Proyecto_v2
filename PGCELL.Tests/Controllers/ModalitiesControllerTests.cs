﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
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

    }
}