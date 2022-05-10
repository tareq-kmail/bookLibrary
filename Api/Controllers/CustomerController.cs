using Domain.Manager;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerManager _customerManager;
        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        [HttpPost]
        [Produces(typeof(CustomerModel))]
        public async Task<IActionResult> AddBook([Required][FromBody] CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerManager.Create(model);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("id")]
        [Produces(typeof(CustomerModel))]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _customerManager.GetById(id);
            return Ok(customer);
        }

        [HttpGet("nationalId")]
        [Produces(typeof(CustomerModel))]
        public async Task<IActionResult> GetByNationalId(string nationalId)
        {
            var customer = await _customerManager.GetByNationalId(nationalId);
            return Ok(customer);
        }
        //[HttpGet]
        //[Produces(typeof(List<BookModel>))]
        //public async Task<IActionResult> GetAll(BookFilterModel filter)
        //{          
        //    var result = await _bookManager.GetAll(filter);
        //    return Ok(result);
        //}
        [HttpGet]
        [Produces(typeof(List<CustomerModel>))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customerManager.GetAll();
            return Ok(result);
        }

        [HttpPut]
        [Produces(typeof(CustomerModel))]
        public async Task<IActionResult> Update([Required][FromBody] CustomerModel model)
        {

            var result = await _customerManager.Update(model);
            return Ok(result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _customerManager.Delete(id);
            return NoContent();
        }
    }
}

