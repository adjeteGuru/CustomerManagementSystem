using CustomerManagementSystem.Core.Models;
using CustomerManagementSystem.Core.Providers;
using Microsoft.AspNetCore.Components;

namespace CustomerManagementSystem.App.Pages
{
    public class CustomerListComponent : ComponentBase
    {
        [Inject]
        public ICustomersDataProvider CustomersDataProvider { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; } = default!;
        
        public List<Customer> Customers { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Customers = new List<Customer>();
                var customers = await CustomersDataProvider.GetAllCustomersAsync();
                Customers.AddRange(customers);
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }

        public void NavigateToCreate()
        {
            Navigation.NavigateTo("/customerlist/create");
        }
    }
}
