using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using PGCELL.Shared.Entites;

namespace PGCELL.Frontend.Pages.Workers
{
    public partial class WorkerForm
    {
        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;

        private EditContext editContext = null!;

        protected override void OnInitialized()
        {
            editContext = new(Worker);            
        }        

        [EditorRequired]
        [Parameter]
        public Worker Worker { get; set; } = null!;

        /**[Parameter]
        public List<User> AvailableUsers { get; set; }**/

        [Parameter]
        public List<Modality> AvailableModalities { get; set; }

        [Parameter]
        public List<WorkSchedule> AvailableWorkShedules { get; set; }

        [EditorRequired]
        [Parameter]
        public EventCallback OnValidSubmit { get; set; }

        [EditorRequired]
        [Parameter]
        public EventCallback ReturnAction { get; set; }

        public bool FormPostedSuccessfully { get; set; } = false;

        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            var formWasEdited = editContext.IsModified();

            if (!formWasEdited)
            {
                return;
            }

            if (FormPostedSuccessfully)
            {
                return;
            }

            var result = await sweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = "¿Deseas abandonar la página y perder los cambios?",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true
            });

            var confirm = !string.IsNullOrEmpty(result.Value);

            if (confirm)
            {
                return;
            }

            context.PreventNavigation();
        }

        /**private void UpdateUser(ChangeEventArgs e)
        {
            var selectedUserId = Convert.ToInt32(e.Value);
            Worker.User = AvailableUsers.FirstOrDefault(u => u.Document == selectedUserId.ToString());
        }**/

        private void UpdateSelectedModality(ChangeEventArgs e)
        {
            var selectedModalityId = Convert.ToInt32(e.Value);
            Worker.Modality = AvailableModalities.FirstOrDefault(m => m.Id == selectedModalityId);
        }

        private void UpdateSelectedWorkShedule(ChangeEventArgs e)
        {
            var selectedWorkSheduleId = Convert.ToInt32(e.Value);
            Worker.WorkSchedule = AvailableWorkShedules.FirstOrDefault(w => w.Id == selectedWorkSheduleId);
        }
    }
}
