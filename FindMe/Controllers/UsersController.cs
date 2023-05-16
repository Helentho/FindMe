using AutoMapper;
using FindMe.DTO;
using FindMe.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;


namespace FindMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        ApplicationContext db;
        private readonly IMapper mapper;

        public UsersController(ApplicationContext context, IMapper mapper)
        {
            db = context;
            this.mapper = mapper;

            if (!db.Users.Any())
            {
                db.Users.Add(new User { Email = "exemple1@gmail.com", Password = "26weGvftgh", PhoneNumber = "2212365456"});
                db.Users.Add(new User { Email = "exemple2@gmail.com", Password = "55erkiEoljf", PhoneNumber = "5897416987", Active = false });
                db.Users.Add(new User { Email = "exemple3@gmail.com", Password = "99Rfvbgtt", PhoneNumber = "8855214778"});

                db.SaveChanges();
            }
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
             return await db.Users
                           .Where(p => p.Active)
                           .Select(p => mapper.Map<UserDTO>(p))
                           .ToListAsync();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound();

            return new ObjectResult(mapper.Map<UserDTO>(user));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO user)
        {
            if (user == null)
            {
                return BadRequest("The user cannot be null.");
            }

            try
            {
                db.Users.Add(mapper.Map<User>(user));
                await db.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDTO user)
        {
            if (user == null || id < 1)
            {
                return BadRequest("The user cannot be null. ID must be greater than zero.");
            }

            var oldCreatedDate = await db.Users.Where(x => x.Id == id && x.Active)
                                               .Select(p => p.CreatedDate)
                                               .FirstOrDefaultAsync();

            if (oldCreatedDate == new DateTime())
            {
                return NotFound();
            }

            try
            {
                var userToUpdate = mapper.Map<User>(user);
                userToUpdate.Id = id;
                userToUpdate.CreatedDate = oldCreatedDate;
                db.Users.Update(userToUpdate);
                await db.SaveChangesAsync();
                return Ok(userToUpdate);
            }
            catch (DbUpdateException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Activate(int id)
        {
            var checkedUser = await db.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (checkedUser == null)
            {
                return BadRequest("Not found");
            }

            checkedUser.Active = true;
            await db.SaveChangesAsync();

            return Ok(mapper.Map<UserDTO>(checkedUser));
        }

        //// DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userToDelete = await db.Users.FirstOrDefaultAsync(x => x.Id == id);

            if(userToDelete == null)
            {
                return BadRequest("Not found");
            }

            userToDelete.Active = false;
            await db.SaveChangesAsync();

            return Ok(mapper.Map<UserDTO>(userToDelete));
        }
    }
}
