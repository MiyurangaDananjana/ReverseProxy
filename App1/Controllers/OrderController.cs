using App1.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App1.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private static readonly List<OrderModel> _orders = new List<OrderModel>()
        {
            new OrderModel { Id = 1, CustomerName = "John Doe", TotalAmount = 100 },
            new OrderModel { Id = 2, CustomerName = "Jane Smith", TotalAmount = 50 },
            new OrderModel { Id = 3, CustomerName = "David Lee", TotalAmount = 150 }
        };

        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(_orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
