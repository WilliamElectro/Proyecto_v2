using Microsoft.AspNetCore.Components;
using PGCELL.Frontend.Services;

namespace PGCELL.Frontend.Pages.Auth
{
    public partial class Logout
    {
        [Inject] private NavigationManager navigationManager { get; set; } = null!;

        [Inject] private ILoginService loginService { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await loginService.LogoutAsync();
            navigationManager.NavigateTo("/");
        }
    }
}