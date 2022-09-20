using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizza_mama.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pizza_mama.Controllers
{
    [Route("[controller]")]
    public class APIController : Controller
    {

        private readonly pizza_mama.Data.DataContext _context;
        public IList<Pizza> Pizza { get; set; } = default!;

        public APIController(pizza_mama.Data.DataContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        [Route("GetPizza")]
        public IActionResult GetPizza()
        {
            if (_context.Pizzas != null)
            {
                Pizza = _context.Pizzas.ToList();
            }
            //var pizza = new Pizza() { nom = "pizza test", prix = 8, vegetarienne = false, ingredients = "tomate, oignons, oeuf" };

            return Json(Pizza);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

