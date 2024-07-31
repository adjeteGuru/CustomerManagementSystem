using CustomerManagementSystem.Core.DTOs;
using Microsoft.AspNetCore.Components;

namespace CustomerManagementSystem.App.Pages
{
    public class CustomersComponent : ComponentBase
    {
        [Parameter]
        public IEnumerable<CustomerRead> Customers { get; set; }
    }
}
