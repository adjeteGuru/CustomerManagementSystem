using CustomerManagementSystem.Core.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CustomerManagementSystem.App.Pages
{
    public class CreateCustomerComponent : ComponentBase
    {
        public UpdateCustomerModal CustomerModal { get; set; }

        public EditContext EditContext;

        public Customer Customer { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            Customer = new Customer();
            EditContext = new EditContext(Customer);
        }
    }

    
}
