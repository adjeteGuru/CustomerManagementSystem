using CustomerManagementSystem.Core.DTOs;
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
        
        public List<CustomerRead> Customers { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Customers = new List<CustomerRead>();
                var customers = await CustomersDataProvider.GetAllCustomersAsync();
                Customers.AddRange(customers);
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }

        protected IOrderedEnumerable<IGrouping<int, CustomerRead>> GetGroupedCustomersByDepartment()
        {
            var data = from customer in Customers
                       group customer by customer.DepartmentId into customerByDepartGroup
                       orderby customerByDepartGroup.Key
                       select customerByDepartGroup;
            return data;
        }

        protected string GetDepartmentName(IGrouping<int, CustomerRead> groupedCustomers)
        {
            return groupedCustomers.FirstOrDefault(x => x.DepartmentId == groupedCustomers.Key)!.DepartmentName;
        }

        public void NavigateToCreate()
        {
            Navigation.NavigateTo("/customerlist/create");
        }

        public async Task AddCustomer(CustomerAddDTo customerAdd)
        {
            //await CustomersDataProvider.RemoveCustomerAsync(id);
            var result = await CustomersDataProvider.CreateCustomerAsync(customerAdd);
            StateHasChanged();
            Navigation.NavigateTo("/customerlist");
        }
    }
}
