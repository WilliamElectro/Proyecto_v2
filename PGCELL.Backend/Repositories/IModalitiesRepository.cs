﻿using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;


namespace PGCELL.Backend.Repositories
{
    public interface IModalitiesRepository
    {
        Task<Response<IEnumerable<Modality>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<Modality>> GetComboAsync();
    }
}
