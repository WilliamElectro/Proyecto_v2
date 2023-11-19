﻿using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.UnitsOfWork
{
    public interface ITemporalOrdersUnitOfWork
    {
        Task<Response<TemporalOrderDTO>> AddFullAsync(string email, TemporalOrderDTO temporalOrderDTO);

        Task<Response<IEnumerable<TemporalOrder>>> GetAsync(string email);

        Task<Response<int>> GetCountAsync(string email);

        Task<Response<TemporalOrder>> GetAsync(int id);

        Task<Response<TemporalOrder>> PutFullAsync(TemporalOrderDTO temporalOrderDTO);

        Task<Response<TemporalOrder>> DeleteAsync(int id);
    }
}