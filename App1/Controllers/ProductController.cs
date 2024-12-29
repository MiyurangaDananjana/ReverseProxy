using App1.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace App1.Controllers
{
    [Route("api/products")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;


        private static readonly List<ProductModel> _products = new List<ProductModel>()
        {
            new ProductModel { Id = 1, Name = "Laptop", Price = 1200 },
            new ProductModel { Id = 2, Name = "Phone", Price = 800 },
            new ProductModel { Id = 3, Name = "Tablet", Price = 400 }
        };

        public ProductController(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _db = _redis.GetDatabase();
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_products);
        }


        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            // Increment search count in Redis
            _db.StringIncrement($"product:{id}:searches");

            return Ok(product);
        }

        [HttpGet("most-searched")]
        public IActionResult GetMostSearchedProduct()
        {
            var mostSearchedProductId = _products
                .Select(p => new
                {
                    Product = p,
                    Searches = (int?)_db.StringGet($"product:{p.Id}:searches") ?? 0
                })
                .OrderByDescending(p => p.Searches)
                .FirstOrDefault();

            if (mostSearchedProductId == null)
            {
                return NotFound("No product searches recorded.");
            }

            return Ok(mostSearchedProductId.Product);
        }
    }
}
