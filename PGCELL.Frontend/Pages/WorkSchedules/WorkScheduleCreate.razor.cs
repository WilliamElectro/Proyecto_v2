﻿using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PGCELL.Frontend.Repositories;
using PGCELL.Shared.Entites;
using System.Threading.Tasks;

namespace PGCELL.Frontend.Pages.WorkSchedules
{
    [Authorize(Roles = "Admin")]
    public partial class WorkScheduleCreate
    {
        [Inject] private NavigationManager navigationManager { get; set; } = null!;
        [Inject] private IRepository repository { get; set; } = null!;
        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;

        private WorkSchedule workSchedule = new();
        private WorkScheduleForm? workScheduleForm;

        [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;

        private async Task CreateAsync()
        {
            var httpResponse = await repository.PostAsync("api/workschedules", workSchedule);

            if (httpResponse.Error)
            {
                var errorMessage = await httpResponse.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", errorMessage, SweetAlertIcon.Error);
                return;
            }

            await BlazoredModal.CloseAsync(ModalResult.Ok());
            Return();

            var toast = sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con éxito.");
        }

        private void Return()
        {
            workScheduleForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo("/workschedules");
        }
    }
}
