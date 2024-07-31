using AutoMapper;
using CustomerManagementSystem.Core.DTOs;
using CustomerManagementSystem.Core.Models;

namespace CustomerManagementSystem.Core.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerRead>();
            CreateMap<CustomerRead, Customer>();
            CreateMap<CustomerAddDTo, Customer>();
            CreateMap<Customer, CustomerAddDTo>();
            CreateMap<CustomerUpdateDto, Customer>();
            CreateMap<Customer, CustomerUpdateDto>();
        }
    }
}
