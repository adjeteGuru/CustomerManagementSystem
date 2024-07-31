using CustomerManagementSystem.Core.DTOs;
using CustomerManagementSystem.Core.Models;
using CustomerManagementSystem.Core.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CustomerManagementSystem.App.Pages
{
    public class CustomerDetailComponent : ComponentBase
    {
        [Inject] 
        public ICustomersDataProvider CustomersDataProvider { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; } = default!;
        
        public Customer Customer { get; set; }

        [Parameter] 
        public EditContext EditContext { get; set; }

        [Parameter]
        public int Id { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Customer = new Customer();
            EditContext = new EditContext(Customer);

            try
            {
                Customer = await CustomersDataProvider.GetCustomerByIdAsync(Id);
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }
       
        public async Task ProcessCustomerUpdate()
        {
            await RunUpdateDetails();
            Navigation.NavigateTo("/customerlist");
        }

        private async Task RunUpdateDetails()
        {
            var customerDto = new CustomerUpdateDto
            {
                Id = Customer.Id,
                FullName = Customer.FullName,
                Address = Customer.Address,
                PostCode = Customer.PostCode,
                Telephone = Customer.Telephone
            };

            await CustomersDataProvider.UpdateCustomerAsync(customerDto);
        }

        public void Cancel()
        {
            Navigation.NavigateTo("/customerlist");
        }
    }
}
