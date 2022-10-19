using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria.Models;
//using la_mia_pizzeria.Data;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace la_mia_pizzeria.Controllers.api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        // GET: /<controller>/

        PizzeriaContext _ctx;
        public PizzaController()
        {
            _ctx = new PizzeriaContext();
        }

        //api/post/get/[qualunque stringa]
        [HttpGet]
        public IActionResult Get(string? nome)
        {
            IQueryable<Pizza> pizzas;

            if (nome != null)
            {
                pizzas = _ctx.Pizzas.Include("Category").Include("Ingredients").Where(pizza => pizza.Nome.ToLower().Contains(nome.ToLower()));
            }
            else
            {
                pizzas = _ctx.Pizzas.Include("Category").Include("Ingredients");
            }

            return Ok(pizzas.ToList<Pizza>());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Pizza pizza = _ctx.Pizzas.Include("Category").Include("Ingredients").Where(e => e.Id == id).FirstOrDefault();
            //List<Actor> actors = _ctx.Actors.ToList();
            return Ok(pizza);
        }
    }

    // METODO PUT

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Pizza model)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }
        using (PizzeriaContext _ctx = new PizzeriaContext())
        {
            // cerchiamo il dato
            Pizza pizzaDaModificare = _ctx.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();
            if (pizzaDaModificare != null)
            {
                // ... aggiorniamo il nostro dato a DB
                return Ok(pizzaDaModificare);
            }
            else
            {
                // se non è stato trovato resituiamo che non esiste
                return NotFound();
            }
        }
    }

 
    // METODO DELETE

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        using (PizzeriaContext _ctx = new PizzeriaContext())
        {
            Pizza pizzaDaRimuovere = _ctx.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();
            if (pizzaDaRimuovere != null)
            {
                _ctx.Pizzas.Remove(pizzaDaRimuovere);
                _ctx.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}