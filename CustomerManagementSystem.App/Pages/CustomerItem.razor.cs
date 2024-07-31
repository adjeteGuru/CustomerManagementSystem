using CustomerManagementSystem.Core.Models;
using CustomerManagementSystem.Core.Providers;
using Microsoft.AspNetCore.Components;

namespace CustomerManagementSystem.App.Pages
{
    public class CustomerItemComponent : ComponentBase
    {
        [Inject]
        public ICustomersDataProvider CustomersDataProvider { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; } = default!;

        [Parameter] 
        public Customer Customer { get; set; }

        public async Task RemoveCustomer(int id)
        {
            await CustomersDataProvider.RemoveCustomerAsync(id);
            StateHasChanged();
            Navigation.NavigateTo("/customerlist");
        }
    }
}
