using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using PGCELL.Frontend.Repositories;
using PGCELL.Shared.Entites;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PGCELL.Frontend.Pages.SendMessage
{
    public partial class SendMessageIndex : ComponentBase
    {
        [EditorRequired]
        [Parameter]
        public List<Worker> AvailableWorkes { get; set; } = new List<Worker>();

        [Inject] private IRepository repository { get; set; } = null!;
        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;
        [Inject] private HttpClient httpClient { get; set; } = null!; // Agrega la inyección de HttpClient

        private Worker workerSelected = new Worker();
        private string recipientEmail;
        private string subjectEmail;
        private string message;
        private bool mensajeEnviado;

        protected override async Task OnInitializedAsync()
        {
            await LoadWorkersAsync();
        }

        private async Task LoadWorkersAsync()
        {
            var responseHttp = await repository.GetAsync<List<Worker>>("/api/workers/combo");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            AvailableWorkes = responseHttp.Response;
        }

        private void UpdateSelectedWorker(ChangeEventArgs e)
        {
            var selectedWorkerId = Convert.ToInt32(e.Value);
            workerSelected = AvailableWorkes.FirstOrDefault(m => m.Id == selectedWorkerId);

            if (workerSelected != null)
            {
                recipientEmail = workerSelected.Email;
            }
        }

        private async Task EnviarCorreo()
        {
            try
            {
                var emailData = new
                {
                    RecipientEmail = recipientEmail,
                    Subject = subjectEmail,
                    Message = message
                };

                var response = await httpClient.PostAsJsonAsync("/api/sendmail/send", emailData);

                if (response.IsSuccessStatusCode)
                {
                    mensajeEnviado = true;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Error al enviar el correo electrónico: " + errorMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo electrónico: " + ex.Message);
            }
        }
    }
}
