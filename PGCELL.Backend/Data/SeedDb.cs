using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using PGCELL.Backend.Helpers;
using PGCELL.Backend.Services;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Enums;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IApiService _apiService;
        private readonly IUserHelper _userHelper;
        private readonly IFileStorage _fileStorage;
        private readonly IRuntimeInformationWrapper _runtimeInformationWrapper;

        public SeedDb(DataContext context, IApiService apiService, IUserHelper userHelper, IFileStorage fileStorage, IRuntimeInformationWrapper runtimeInformationWrapper)
        {
            _context = context;
            _apiService = apiService;
            _userHelper = userHelper;
            _fileStorage = fileStorage;
            _runtimeInformationWrapper = runtimeInformationWrapper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            //await CheckCountriesAsync();
            await CheckCountriesAsync2();
            await CheckRolesAsync();
            //await CheckUserAsync("0001", "Juan", "Zuluaga", "zulu@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "JuanZuluaga.jpg", UserType.Admin);
            await CheckUserAsync("0001", "William", "Bohorquez", "williambohorquezgutierrez2@gmail.com", "320 476 3486", "Calle 1 Carrera 1", "bob.jpg", UserType.Admin);
            await CheckUserAsync("0002", "Jenny", "Chavez", "jennycaro13@gmail.com", "320 456 6789", "Calle 40 Carrera 30", "selena.jpg", UserType.Admin);
            //await CheckUserAsync("0002", "Ledys", "Bedoya", "ledys@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "LedysBedoya.jpg", UserType.User);
            await CheckWorkSchedulesAsync();
            await CheckWorkersAsync();
            await CheckTypeNoveltyAsync();
            await CheckNoveltyAsync();
            await CheckContractAsync();
            await CheckModalitiesAsync();

        }

        private async Task CheckWorkSchedulesAsync()
        {
            if (!_context.WorkSchedules.Any())
            {
                _context.WorkSchedules.Add(new WorkSchedule { Name = "Mañana" });
                _context.WorkSchedules.Add(new WorkSchedule { Name = "Tarde" });
                _context.WorkSchedules.Add(new WorkSchedule { Name = "Noche" });
                 
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckWorkersAsync()
        {
            if (!_context.Workers.Any())
            {
                _context.Workers.Add(new Worker { Document = "1075 1", FirstName = "William" , LastName = "Bohorquez"});
                _context.Workers.Add(new Worker { Document = "1075 2", FirstName = "Sergio", LastName = "" });
                _context.Workers.Add(new Worker { Document = "1075 3", FirstName = "Jenny" , LastName = "" });
                _context.Workers.Add(new Worker { Document = "1075 4", FirstName = "Karen" , LastName = "" });
                _context.Workers.Add(new Worker { Document = "1075 5", FirstName = "Lucas" , LastName = "" });
                _context.Workers.Add(new Worker { Document = "1075 6", FirstName = "Luis", LastName = "" });
                _context.Workers.Add(new Worker { Document = "1075 7", FirstName = "Martha", LastName = "" });
                _context.Workers.Add(new Worker { Document = "1075 8", FirstName = "Miguel", LastName = "" });
                _context.Workers.Add(new Worker { Document = "1075 9", FirstName = "Patricia", LastName = "" });
                _context.Workers.Add(new Worker { Document = "1075 10", FirstName = "Carmen", LastName = "" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCountriesAsync2()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States = new List<State>
                    {
                        new State
                        {
                            Name = "Antioquia",
                            Cities = new List<City>
                            {
                                new City
                                {
                                    Name = "Medellín"
                                }
                            }
                        }
                    }
                });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckTypeNoveltyAsync()
        {
            if (!_context.TypeNovelties.Any())
            {
                _context.TypeNovelties.Add(new TypeNovelty { Name = "Vacaciones" });
                _context.TypeNovelties.Add(new TypeNovelty { Name = "Licencia por maternidad" });
                _context.TypeNovelties.Add(new TypeNovelty { Name = "Licencia por paternidad" });
                _context.TypeNovelties.Add(new TypeNovelty { Name = "Licencia por grave calamidad doméstica" });
                _context.TypeNovelties.Add(new TypeNovelty { Name = "Licencia por luto" });
                _context.TypeNovelties.Add(new TypeNovelty { Name = "Licencia para entierro de compañeros" });
                _context.TypeNovelties.Add(new TypeNovelty { Name = "Licencia como consecuencia del desempeño de cargos oficiales" });
                _context.TypeNovelties.Add(new TypeNovelty { Name = "Licencia para ejercer el derecho al voto" });
                _context.TypeNovelties.Add(new TypeNovelty { Name = "Licencia sindical" });
                _context.TypeNovelties.Add(new TypeNovelty { Name = "Permiso sindical" });
                _context.TypeNovelties.Add(new TypeNovelty { Name = "Permiso de lactancia" });
                _context.TypeNovelties.Add(new TypeNovelty { Name = "Permiso académico compensado" });
                _context.TypeNovelties.Add(new TypeNovelty { Name = "Permiso para ejercer la docencia universitaria" });
                _context.TypeNovelties.Add(new TypeNovelty { Name = "Descansos" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckNoveltyAsync()
        {
            if (!_context.Novelties.Any())
            {
                _context.Novelties.Add(new Novelty { Name = "Licencia por grave calamidad doméstica - Juan" });
                _context.Novelties.Add(new Novelty { Name = "Licencia por maternidad - Maria" });
                _context.Novelties.Add(new Novelty { Name = "Licencia por paternidad - José" });
                _context.Novelties.Add(new Novelty { Name = "Licencia por grave calamidad doméstica _ Angela" });
                _context.Novelties.Add(new Novelty { Name = "Licencia por luto - Marcos" });
                _context.Novelties.Add(new Novelty { Name = "Licencia para entierro de compañeros - Morena" });
                _context.Novelties.Add(new Novelty { Name = "Licencia como consecuencia del desempeño de cargos oficiales - Jenny" });
                _context.Novelties.Add(new Novelty { Name = "Licencia para ejercer el derecho al voto - William" });
                _context.Novelties.Add(new Novelty { Name = "Licencia sindical - Sergio" });
                _context.Novelties.Add(new Novelty { Name = "Permiso sindical - Sergio" });
                _context.Novelties.Add(new Novelty { Name = "Permiso de lactancia -Nubia" });
                _context.Novelties.Add(new Novelty { Name = "Permiso académico compensado - Jenny" });
                _context.Novelties.Add(new Novelty { Name = "Permiso para ejercer la docencia universitaria - Jenny" });
                _context.Novelties.Add(new Novelty { Name = "Descansos - William" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckContractAsync()
        {
            if (!_context.Contracts.Any())
            {
                _context.Contracts.Add(new Shared.Entites.Contract { Name = "Contrato de Juan" });
                _context.Contracts.Add(new Shared.Entites.Contract { Name = "Contrato de Maria" });
                _context.Contracts.Add(new Shared.Entites.Contract { Name = "Contrato de José" });
                _context.Contracts.Add(new Shared.Entites.Contract { Name = "Contrato de Angela" });
                _context.Contracts.Add(new Shared.Entites.Contract { Name = "Contrato de Marcos" });
                _context.Contracts.Add(new Shared.Entites.Contract { Name = "Contrato de Morena" });
                _context.Contracts.Add(new Shared.Entites.Contract { Name = "Contrato de Jenny" });
                _context.Contracts.Add(new Shared.Entites.Contract { Name = "Contrato de William" });
                _context.Contracts.Add(new Shared.Entites.Contract { Name = "Contrato de Sergio" });
                _context.Contracts.Add(new Shared.Entites.Contract { Name = "Contrato de Dario" });
                _context.Contracts.Add(new Shared.Entites.Contract { Name = "Contrato de Nubia" });
                _context.Contracts.Add(new Shared.Entites.Contract { Name = "Contrato de Vanessa" });
                _context.Contracts.Add(new Shared.Entites.Contract { Name = "Contrato de Ricardo" });
                _context.Contracts.Add(new Shared.Entites.Contract { Name = "Contrato de Hector" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckModalitiesAsync()
        {
            if (!_context.Modalities.Any())
            {
                _context.Modalities.Add(new Modality { Name = "Contrato por obra o labor" });
                _context.Modalities.Add(new Modality { Name = "Contrato de trabajo a término fijo" });
                _context.Modalities.Add(new Modality { Name = "Contrato de trabajo a término indefinido" });
                _context.Modalities.Add(new Modality { Name = "Contrato de aprendizaje" });
                _context.Modalities.Add(new Modality { Name = "Contrato temporal, ocasional o accidental" });
                _context.Modalities.Add(new Modality { Name = "Modalidad futura 1 contrato por hora" });
                _context.Modalities.Add(new Modality { Name = "Modalidad futura 2 contrato por semana" });
                _context.Modalities.Add(new Modality { Name = "Modalidad futura 3 contrato por mes" });
                _context.Modalities.Add(new Modality { Name = "Modalidad futura 4 contrato por minutos" });
                _context.Modalities.Add(new Modality { Name = "Modalidad futura 5 contrato por terceros" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, string image, UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Name == "Medellín");
                if (city == null)
                {
                    city = await _context.Cities.FirstOrDefaultAsync();
                }

                string filePath;
                if (_runtimeInformationWrapper.IsOSPlatform(OSPlatform.Windows))
                {
                    filePath = $"{Environment.CurrentDirectory}\\Images\\users\\{image}";
                }
                else
                {
                    filePath = $"{Environment.CurrentDirectory}/Images/users/{image}";
                }
                var fileBytes = File.ReadAllBytes(filePath);
                var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "users");

                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = city,
                    UserType = userType,
                    Photo = imagePath,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                var responseCountries = await _apiService.GetAsync<List<CountryResponse>>("/v1", "/countries");
                if (responseCountries.WasSuccess)
                {
                    var countries = responseCountries.Result!;
                    foreach (var countryResponse in countries)
                    {
                        var country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == countryResponse.Name!)!;
                        if (country == null)
                        {
                            country = new() { Name = countryResponse.Name!, States = new List<State>() };
                            var responseStates = await _apiService.GetAsync<List<StateResponse>>("/v1", $"/countries/{countryResponse.Iso2}/states");
                            if (responseStates.WasSuccess)
                            {
                                var states = responseStates.Result!;
                                foreach (var stateResponse in states!)
                                {
                                    var state = country.States!.FirstOrDefault(s => s.Name == stateResponse.Name!)!;
                                    if (state == null)
                                    {
                                        state = new() { Name = stateResponse.Name!, Cities = new List<City>() };
                                        var responseCities = await _apiService.GetAsync<List<CityResponse>>("/v1", $"/countries/{countryResponse.Iso2}/states/{stateResponse.Iso2}/cities");
                                        if (responseCities.WasSuccess)
                                        {
                                            var cities = responseCities.Result!;
                                            foreach (var cityResponse in cities)
                                            {
                                                if (cityResponse.Name == "Mosfellsbær" || cityResponse.Name == "Șăulița")
                                                {
                                                    continue;
                                                }
                                                var city = state.Cities!.FirstOrDefault(c => c.Name == cityResponse.Name!)!;
                                                if (city == null)
                                                {
                                                    state.Cities.Add(new City() { Name = cityResponse.Name! });
                                                }
                                            }
                                        }
                                        if (state.CitiesNumber > 0)
                                        {
                                            country.States.Add(state);
                                        }
                                    }
                                }
                            }
                            if (country.StatesNumber > 0)
                            {
                                _context.Countries.Add(country);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
        }
    }
}