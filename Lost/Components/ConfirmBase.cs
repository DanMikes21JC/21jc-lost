using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Lost.Components
{
    public class ConfirmBase : ComponentBase
    {
        protected bool ShowConfirmation { get; set; }
        protected bool ShowUnable { get; set; }

        [Parameter]
        public string ConfirmationTitle { get; set; } = "Confirmation de suppression";

        [Parameter]
        public string ConfirmationMessage { get; set; } = "Êtes-vous sûr de vouloir supprimer ?";

        [Parameter]
        public string UnableTitle { get; set; } = "Impossible de supprimer";

        [Parameter]
        public string UnableMessage { get; set; } = "L'élément est utilisé et ne peut pas être supprimé";

        public void Show()
        {
            ShowConfirmation = true;
            ShowUnable = false;
            StateHasChanged();
        }

        public void ShowUnableDelete()
        {
            ShowConfirmation = false;
            ShowUnable = true;
            StateHasChanged();
        }

        [Parameter]
        public EventCallback<bool> ConfirmationChanged { get; set; }

        protected async Task OnConfirmationChange(bool value)
        {
            ShowConfirmation = false;
            ShowUnable = false;
            await ConfirmationChanged.InvokeAsync(value);
        }
    }
}
