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
    [Route("api/sales")]
    public class SalesController : Controller
    {
        
        private readonly ISalesManager _salesManager;
       
        public SalesController(ISalesManager salesManager)
        {
            
            _salesManager = salesManager;
            
        }

        [HttpPost]
        [Produces(typeof(SalesModel))]
        public async Task<IActionResult> AddSales([Required][FromBody] SalesModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _salesManager.Create(model);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        [Produces(typeof(List<SalesModel>))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _salesManager.GetAll();
            return Ok(result);
        }

        [HttpGet("id")]
        [Produces(typeof(SalesModel))]
        public async Task<IActionResult> GetById(Guid id)
        {
            var sale = await _salesManager.GetById(id);
            return Ok(sale);
        }

        [HttpGet("bookId")]
        [Produces(typeof(List<SalesModel>))]
        public async Task<IActionResult>/*<List<SalesModel>>*/ GetByBookId(Guid id)
        {
            var sales = await _salesManager.GetByBookId(id);
            return Ok(sales);
        }

        [HttpGet("customerId")]
        [Produces(typeof(List<SalesModel>))]
        public async Task<IActionResult> GetByCustomerId(Guid id)
        {
            var sales = await _salesManager.GetByCustomerId(id);
            return Ok(sales);
        }

        [HttpPut]
        [Produces(typeof(SalesModel))]
        public async Task<IActionResult> Update([Required][FromBody] SalesModel model)
        {

            var result = await _salesManager.Update(model);
            return Ok(result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _salesManager.Delete(id);
            return NoContent();
        }
    }
}
