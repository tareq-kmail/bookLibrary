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
    [Route("api/rentals")]
    public class RentalController : Controller
    {
        private readonly IRentalManager _rentalManager;
        
        public RentalController(IRentalManager rentalManager)
        {
            _rentalManager = rentalManager;
        }

        [HttpPost]
        [Produces(typeof(RentalModel))]
        public async Task<IActionResult> AddRental([Required][FromBody] RentalModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _rentalManager.Create(model);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        [Produces(typeof(List<RentalModel>))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _rentalManager.GetAll();
            return Ok(result);
        }

        [HttpGet("id")]
        [Produces(typeof(RentalModel))]
        public async Task<IActionResult> GetById(Guid id)
        {
            var rental = await _rentalManager.GetById(id);
            return Ok(rental);
        }

        [HttpGet("bookId")]
        [Produces(typeof(List<RentalModel>))]
        public async Task<IActionResult>/*<List<SalesModel>>*/ GetByBookId(Guid id)
        {
            var rentals = await _rentalManager.GetByBookId(id);
            return Ok(rentals);
        }

        [HttpGet("customerId")]
        [Produces(typeof(List<RentalModel>))]
        public async Task<IActionResult> GetByCustomerId(Guid id)
        {
            var rentals = await _rentalManager.GetByCustomerId(id);
            return Ok(rentals);
        }

        [HttpPut]
        [Produces(typeof(RentalModel))]
        public async Task<IActionResult> Update([Required][FromBody] RentalModel model)
        {

            var result = await _rentalManager.Update(model);
            return Ok(result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _rentalManager.Delete(id);
            return NoContent();
        }
    }
}
