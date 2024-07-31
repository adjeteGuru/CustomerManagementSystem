﻿using AutoMapper;
using CustomerManagementSystem.Infrastructure.DTOs;
using CustomerManagementSystem.Infrastructure.Models;
using CustomerManagementSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET api/Customer
        [HttpGet]
        public async Task<IActionResult> GetAllCustomersAsync()
        {
            try
            {
                var customers = await _customerService.GetAllCustomersAsync();

                if (!customers.Any())
                {
                    return NotFound();
                }

                var mappedCustomer = _mapper.Map<IEnumerable<CustomerRead>>(customers);
                return Ok(mappedCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/Customer/5 id
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);

                return Ok(_mapper.Map<CustomerRead>(customer));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/Customer
        [HttpPost]
        public async Task<IActionResult> CreateNewCustomerAsync([FromBody] CustomerAddDTo customerAdd)
        {
            try
            {
                var customer = _mapper.Map<Customer>(customerAdd);

                _customerService.AddCustomer(customer);

                var newCustomerDto = _mapper.Map<CustomerRead>(customer);

                return CreatedAtAction(nameof(GetCustomerById), new { id = newCustomerDto.Id }, newCustomerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerAsync(int id, CustomerUpdateDto customerUpdateDto)
        {
            try
            {
                if (id != customerUpdateDto.Id)
                {
                    BadRequest();
                }

                var customer = _mapper.Map<Customer>(customerUpdateDto);

                _customerService.UpdateCustomer(customer);
              
                return Ok(_mapper.Map<CustomerRead>(customer));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerByIdAsync(int id)
        {
            try
            {
                var customerToDelete = await _customerService.GetCustomerByIdAsync(id);

                if (customerToDelete == null)
                {
                    throw new Exception("Something went wrong when attempting to retrieve the customer.");
                }

                await _customerService.DeleteCustomerAsync(customerToDelete.Id);

                var customer = _mapper.Map<CustomerRead>(customerToDelete);

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}