﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria.Models;
//using la_mia_pizzeria.Data;
using Microsoft.EntityFrameworkCore;

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
}