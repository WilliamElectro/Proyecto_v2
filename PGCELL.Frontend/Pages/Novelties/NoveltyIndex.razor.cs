﻿using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PGCELL.Frontend.Repositories;
using PGCELL.Shared.Entites;

namespace PGCELL.Frontend.Pages.Novelties
{
    //[Authorize(Roles = "Admin")]
    public partial class NoveltyIndex : ComponentBase
    {
        [Inject] private IRepository repository { get; set; } = null!;
        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;
        [CascadingParameter] private IModalService Modal { get; set; } = default!;

        public List<Novelty>? Novelties { get; set; }
        private int currentPage = 1;
        private int totalPages;
        private string Filter { get; set; } = string.Empty;
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; } = null!;
        private bool isAuthenticated;

        protected override async Task OnInitializedAsync()
        {
            await CheckIsAuthenticatedAsync();
            await LoadAsync();
        }
        private async Task CheckIsAuthenticatedAsync()
        {
            var authenticationState = await authenticationStateTask;
            isAuthenticated = authenticationState.User.Identity!.IsAuthenticated;
        }

        private async Task ShowModal(int id = 0, bool isEdit = false)
        {
            IModalReference modalReference;

            if (isEdit)
            {
                modalReference = Modal.Show<NoveltyEdit>(string.Empty, new ModalParameters().Add("Id", id));
            }
            else
            {
                modalReference = Modal.Show<NoveltyCreate>();
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
            var url = $"api/novelties?page={page}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var response = await repository.GetAsync<List<Novelty>>(url);
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            Novelties = response.Response;
            return true;
        }

        private async Task LoadPagesAsync()
        {
            var url = "api/novelties/totalPages";
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

        private async Task DeleteAsync(Novelty novelty)
        {
            var result = await sweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = $"¿Estás seguro que quieres borrar la novedad: {novelty.Name}?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });

            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            var response = await repository.DeleteAsync($"api/novelties/{novelty.Id}");
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
