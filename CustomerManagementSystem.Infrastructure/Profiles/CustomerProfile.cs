using CustomerManagementSystem.Infrastructure.DTOs;
using CustomerManagementSystem.Infrastructure.Models;
using AutoMapper;

namespace CustomerManagementSystem.Infrastructure.Profiles
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
