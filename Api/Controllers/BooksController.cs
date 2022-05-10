using Domain.Manager;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CrmEarlyBound;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace Api.Controllers
{
    [Route("api/books")]
    public class BooksController : Controller
    {
        private readonly IBookManager _bookManager;
        public BooksController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        [HttpPost]
        [Produces(typeof(BookModel))]
        public async Task<IActionResult> AddBook([Required][FromBody] BookModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _bookManager.Create(model);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("id")]
        [Produces(typeof(BookModel))]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _bookManager.GetById(id);
            return Ok(book);
        }
        [HttpGet("isbn")]
        [Produces(typeof(BookModel))]
        public async Task<IActionResult> GetByIsbn(string isbn)
        {
            var book = await _bookManager.GetByIsbn(isbn);
            return Ok(book);
        }

        //[HttpGet]
        //[Produces(typeof(List<BookModel>))]
        //public async Task<IActionResult> GetAll(BookFilterModel filter)
        //{          
        //    var result = await _bookManager.GetAll(filter);
        //    return Ok(result);
        //}
        [HttpGet]
        [Produces(typeof(List<BookModel>))]
        public async Task<IActionResult> GetAll()
        {          
            var result = await _bookManager.GetAll();
            return Ok(result);
        }

        [HttpPut]
        [Produces(typeof(BookModel))]
        public async Task<IActionResult> Update([Required][FromBody] BookModel model)
        {
           
            var result = await _bookManager.Update(model);
            return Ok(result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(Guid id)
        {      
            _bookManager.Delete(id);
            return NoContent();
        }
    }
}
