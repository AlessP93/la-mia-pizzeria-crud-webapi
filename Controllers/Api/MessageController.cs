using la_mia_pizzeria.Models;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;


namespace la_mia_pizzeria.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        [HttpPost]
        public IActionResult Send([FromBody] Message message)
        {

            PizzeriaContext ctx = new PizzeriaContext();

            ctx.Messages.Add(message);
            ctx.SaveChanges();

            return Ok();
        }


    }



    //from body - differenze con altri dati e chiamate
    //validation: possiamo entrare nella action?
    //dettagli tra {} - [] nelle pattern matching delle rotte
}
