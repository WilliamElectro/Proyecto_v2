using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PGCELL.Frontend.Repositories;
using PGCELL.Shared.Entites;

namespace PGCELL.Frontend.Pages.Workers
{
    //[Authorize(Roles = "Admin")]
    public partial class WorkerIndex : ComponentBase
    {
        [Inject] private IRepository repository { get; set; } = null!;
        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;
        [CascadingParameter] private IModalService Modal { get; set; } = default!;

        public List<Worker>? Workers { get; set; }
        private int currentPage = 1;
        private int totalPages;
        private string Filter { get; set; } = string.Empty;
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; } = null!;
        private bool isAuthenticated;

        [EditorRequired]
        [Parameter]
        public List<Modality> AvailableModalities { get; set; } = new List<Modality>();

        [EditorRequired]
        [Parameter]
        public List<WorkSchedule> AvailableWorkShedules { get; set; } = new List<WorkSchedule>();

        //private int selectedModalityId; // Campo para almacenar el ID de la modalidad seleccionada

        protected override async Task OnInitializedAsync()
        {
            await CheckIsAuthenticatedAsync();
            await LoadAsync();
            await LoadModalitiesAsync();
            await LoadWorkSheduleAsync();
        }

        private async Task CheckIsAuthenticatedAsync()
        {
            var authenticationState = await authenticationStateTask;
            isAuthenticated = authenticationState.User.Identity!.IsAuthenticated;
        }

        private async Task LoadModalitiesAsync()
        {
            var responseHttp = await repository.GetAsync<List<Modality>>("/api/modalities/combo");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            AvailableModalities = responseHttp.Response;
        }

        private async Task LoadWorkSheduleAsync()
        {
            var responseHttp = await repository.GetAsync<List<WorkSchedule>>("/api/workSchedules/combo");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            AvailableWorkShedules = responseHttp.Response;
        }

        private async Task ShowModal(int id = 0, bool isEdit = false)
        {
            IModalReference modalReference;

            if (isEdit)
            {                
                modalReference = Modal.Show<WorkerEdit>(
                    string.Empty,
                    new ModalParameters
                    {
                        { "Id", id },
                        { "AvailableModalities", AvailableModalities },
                        { "AvailableWorkShedules", AvailableWorkShedules }
                    });
            }
            else
            {                
                modalReference = Modal.Show<WorkerCreate>(string.Empty, new ModalParameters().Add("AvailableModalities", AvailableModalities));
                modalReference = Modal.Show<WorkerCreate>(
                    string.Empty,
                    new ModalParameters
                    {
                        { "AvailableModalities", AvailableModalities },
                        { "AvailableWorkShedules", AvailableWorkShedules }
                    });
            }

            var result = await modalReference.Result;
            if (result.Confirmed)
            {
                await LoadAsync();
            }
        }

        private async Task SelectedPageAsync(int page)
        {
            currentPage = page;
            await LoadAsync(page);
        }

        private async Task LoadAsync(int page = 1)
        {
            if (!string.IsNullOrWhiteSpace(Filter) && int.TryParse(Filter, out int parsed))
            {
                // Handle filtering by ID or other specific logic
                // Example: LoadListByIdAsync(parsed);
            }
            else
            {
                var ok = await LoadListAsync(page);
                if (ok)
                {
                    await LoadPagesAsync();
                }
            }
        }

        private async Task<bool> LoadListAsync(int page)
        {
            var url = $"api/workers?page={page}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var response = await repository.GetAsync<List<Worker>>(url);
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            Workers = response.Response;
            return true;
        }

        private async Task LoadPagesAsync()
        {
            var url = "api/workers/totalPages";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"?filter={Filter}";
            }

            var response = await repository.GetAsync<int>(url);
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            totalPages = response.Response;
        }

        private async Task CleanFilterAsync()
        {
            Filter = string.Empty;
            await ApplyFilterAsync();
        }

        private async Task ApplyFilterAsync()
        {
            int page = 1;
            await LoadAsync(page);
            await SelectedPageAsync(page);
        }

        private async Task DeleteAsync(Worker worker)
        {
            var result = await sweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = $"¿Estás seguro que quieres borrar el trabajador: {worker.Document}?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });

            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            var response = await repository.DeleteAsync($"api/workers/{worker.Id}");
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            await LoadAsync();

            var toast = sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro borrado con éxito.");
        }
    }
}