using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersApi.Data;
using OrdersApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersApi.Controllers
{
    [Route("api/User/{UserId}/Order")]
    [Produces("application/json")]
    public class OrderController : Controller
    {
        private readonly OrdersDbContext context;

        public OrderController(OrdersDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Order> Get(int UserId)
        {
            return context.Orders.Where(x => x.UserID == UserId).ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = context.Orders.FirstOrDefault(x => x.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order order, int UserId)
        {
            if (ModelState.IsValid && context.Users.FirstOrDefault(x => x.Id == UserId) != null)
            {
                context.Orders.Add(order);
                context.SaveChanges();
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Order order, int id)
        {
            if (order.Id != id)
            {
                return BadRequest();
            }

            context.Entry(order).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = context.Orders.FirstOrDefault(x => x.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            context.Orders.Remove(order);
            context.SaveChanges();
            return Ok(order);
        }
    }
}
