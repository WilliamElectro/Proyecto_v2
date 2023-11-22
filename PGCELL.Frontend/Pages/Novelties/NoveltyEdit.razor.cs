using System.Net;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PGCELL.Frontend.Repositories;
using PGCELL.Shared.Entites;
using System.Threading.Tasks;
using Blazored.Modal.Services;

namespace PGCELL.Frontend.Pages.Novelties
{
    [Authorize(Roles = "Admin")]
    public partial class NoveltyEdit
    {
        [Inject] private NavigationManager navigationManager { get; set; } = null!;
        [Inject] private IRepository repository { get; set; } = null!;
        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;

        private Novelty? novelty;
        private NoveltyForm? noveltyForm;

        [CascadingParameter]
        private BlazoredModalInstance BlazoredModal { get; set; } = default!;

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var responseHTTP = await repository.GetAsync<Novelty>($"api/novelties/{Id}");

            if (responseHTTP.Error)
            {
                if (responseHTTP.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    navigationManager.NavigateTo("novelties");
                }
                else
                {
                    var errorMessage = await responseHTTP.GetErrorMessageAsync();
                    await sweetAlertService.FireAsync("Error", errorMessage, SweetAlertIcon.Error);
                }
            }
            else
            {
                novelty = responseHTTP.Response;
            }
        }

        private async Task EditAsync()
        {
            var responseHTTP = await repository.PutAsync("api/novelties", novelty);

            if (responseHTTP.Error)
            {
                var errorMessage = await responseHTTP.GetErrorMessageAsync();
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con éxito.");
        }

        private void Return()
        {
            noveltyForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo("/novelties");
        }
    }
}
