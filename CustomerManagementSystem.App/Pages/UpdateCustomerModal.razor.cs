using CustomerManagementSystem.Core.DTOs;
using CustomerManagementSystem.Core.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CustomerManagementSystem.App.Pages
{
    public class UpdateCustomerModalComponent : ComponentBase
    {
        [Inject]
        private NavigationManager Navigation { get; set; } = default!;

        [Inject]
        public ICustomersDataProvider CustomersDataProvider { get; set; }

        [Parameter] public EditContext EditContext { get; set; }
       
        [Parameter]
        public CustomerAddDTo CustomerToAdd { get; set; }

        [Parameter]
        public string ErrorMessage { get; set; }

        public bool IsModalVisible { get; private set; }
        
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            CustomerToAdd = new CustomerAddDTo();
            EditContext = new EditContext(CustomerToAdd);
        }

        public void OpenCustomerModal()
        {
            EditContext = new EditContext(CustomerToAdd);
        }

        public void CloseModal()
        {
            SetModalVisibility(false);
            StateHasChanged();
            Navigation.NavigateTo("/customerlist");
        }

        public void Submit()
        {
            _ = SaveAsync(CustomerToAdd);
            StateHasChanged();
        }

        private async Task SaveAsync(CustomerAddDTo customerAdd)
        {
            var result = await CustomersDataProvider.CreateCustomerAsync(customerAdd);
            if (result == null)
            {
                ErrorMessage = "Customer was not saved";
            }
            else
            {
                Navigation.NavigateTo("/customerlist");
            }
        }

        private void SetModalVisibility(bool isVisible) => IsModalVisible = isVisible;
    }
}
