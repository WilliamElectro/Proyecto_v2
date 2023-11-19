﻿using PGCELL.Backend.Repositories;
using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.UnitsOfWork
{
    public class CountriesUnitOfWork : GenericUnitOfWork<Country>, ICountriesUnitOfWork
    {
        private readonly ICountriesRepository _countriesRepository;

        public CountriesUnitOfWork(IGenericRepository<Country> repository, ICountriesRepository countriesRepository) : base(repository)
        {
            _countriesRepository = countriesRepository;
        }

        public override async Task<Response<IEnumerable<Country>>> GetAsync(PaginationDTO pagination) => await _countriesRepository.GetAsync(pagination);

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _countriesRepository.GetTotalPagesAsync(pagination);

        public override async Task<Response<Country>> GetAsync(int id) => await _countriesRepository.GetAsync(id);

        public async Task<IEnumerable<Country>> GetComboAsync() => await _countriesRepository.GetComboAsync();
    }
}