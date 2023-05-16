using FindMe.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace FindMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : Controller
    {

        ApplicationContext db;

        public PeopleController(ApplicationContext context)
        {
            db = context;

            if (!db.People.Any())
            {
                db.People.Add(new Person { FirstName = "Vasya", LastName= "Petrov", CurrentUserId = 1 });
                db.People.Add(new Person { FirstName = "Masha", LastName = "Ivanova", CurrentUserId = 1 });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get()
        {
            return await db.People.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            var person = await db.People.FirstOrDefaultAsync(x => x.PersonId == id);

            if (person == null)
            {
                return NotFound();
            }

            return new ObjectResult(person);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest("The person cannot be null.");
            }

            db.People.Add(person); 
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest("The person cannot be null.");
            }

            if (!db.People.Any(x => x.PersonId == person.PersonId))
            {
                return NotFound();
            }

            db.People.Update(person);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromBody] Person person)
        {
            var checkedPerson = await db.People.FirstOrDefaultAsync(x => x.PersonId == person.PersonId);

            if (checkedPerson == null)
            {
                return NotFound();
            }

            db.People.Remove(person);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
