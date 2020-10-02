using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrdersApi.Data;
using OrdersApi.Models;

namespace OrdersApi.Controllers
{
    [Route("api/Users")]
    [Produces("application/json")]
    public class UserController : Controller
    {
        private readonly OrdersDbContext context;

        public UserController(OrdersDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return context.Users.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == id);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                context.Users.Add(user);
                context.SaveChanges();
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] User user, int id)
        {
            if(user.Id != id)
            {
                return BadRequest();
            }

            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            context.Users.Remove(user);
            context.SaveChanges();
            return Ok(user);
        }
    }
}
