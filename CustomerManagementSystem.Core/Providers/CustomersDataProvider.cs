using AutoMapper;
using CustomerManagementSystem.Core.DTOs;
using CustomerManagementSystem.Core.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace CustomerManagementSystem.Core.Providers
{
    public class CustomersDataProvider : ICustomersDataProvider
    {
        private const string RequestUri = "api/Customer";
        private readonly HttpClient _client;
        private readonly IMapper _mapper;

        public CustomersDataProvider(HttpClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CustomerRead>> GetAllCustomersAsync()
        {
            var response = await _client.GetAsync(RequestUri);
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var stringContent = await response.Content.ReadAsStringAsync();
                    var customersDto = JsonConvert.DeserializeObject<IEnumerable<CustomerRead>>(stringContent);
                    if (customersDto == null)
                    {
                        throw new Exception("Not found!");
                    }

                    //var customers = _mapper.Map<IEnumerable<Customer>>(customersDto);
                    return customersDto;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return null;
        }

        public async Task<CustomerRead> GetCustomerByIdAsync(int id)
        {
            var response = await _client.GetAsync($"{RequestUri}/{id}");
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var stringContent = await response.Content.ReadAsStringAsync();
                    var customerDto = JsonConvert.DeserializeObject<CustomerRead>(stringContent);
                    if (customerDto == null)
                    {
                        throw new Exception("Not found!");
                    }

                   // var customer = _mapper.Map<Customer>(customerDto);

                    return customerDto;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return null;
        }

        public async Task<CustomerRead> RemoveCustomerAsync(int id)
        {
            var response = await _client.DeleteAsync($"{RequestUri}/{id}");
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var stringContent = await response.Content.ReadAsStringAsync();

                    var customerDto = JsonConvert.DeserializeObject<CustomerRead>(stringContent);

                    if (customerDto == null)
                    {
                        throw new Exception("Not found!");
                    }

                    //var customer = _mapper.Map<Customer>(customerDto);

                    return customerDto;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return null;
        }

        public async Task<Customer> CreateCustomerAsync(CustomerAddDTo customerAdd)
        {
            var response = await _client.PostAsJsonAsync($"{RequestUri}", customerAdd);
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var stringContent = await response.Content.ReadAsStringAsync();

                    var customerDto = JsonConvert.DeserializeObject<CustomerRead>(stringContent);

                    if (customerDto == null)
                    {
                        throw new Exception("Not found!");
                    }

                    var customer = _mapper.Map<Customer>(customerDto);

                    return customer;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return null;
        }

        public async Task<Customer> UpdateCustomerAsync(CustomerUpdateDto customerUpdate)
        {
            var json = JsonConvert.SerializeObject(customerUpdate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var newRequestUri = "api/customer/" + customerUpdate.Id;

            var response = await _client.PutAsync(newRequestUri, content);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var customerDto = JsonConvert.DeserializeObject<CustomerRead>(responseContent);

                    if (customerDto == null)
                    {
                        throw new Exception("Not found!");
                    }

                    var customer = _mapper.Map<Customer>(customerDto);

                    return customer;
                }

                var errorContent = await response.Content.ReadAsStringAsync();

                var failureResponse = JsonConvert.DeserializeObject<FailureResponseModel>(errorContent)?.Detail;

                if (failureResponse != null)
                {
                    throw new Exception(failureResponse);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return null;
        }
    }
}
