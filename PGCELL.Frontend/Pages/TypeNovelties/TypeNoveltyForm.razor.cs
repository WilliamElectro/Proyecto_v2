using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using PGCELL.Shared.Entites;
using System.Threading.Tasks;

namespace PGCELL.Frontend.Pages.TypeNovelties
{
    public partial class TypeNoveltyForm
    {
        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;

        private EditContext editContext = null!;

        protected override void OnInitialized()
        {
            editContext = new(TypeNovelty);
        }

        [EditorRequired]
        [Parameter]
        public TypeNovelty TypeNovelty { get; set; } = null!;

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
                Title = "Confirmation",
                Text = "Do you want to leave the page and lose the changes?",
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
    }
}
