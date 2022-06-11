using book_shop.Data;
using book_shop.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace book_shop.Controllers
{
    [Route("api/[controller]")]
    public class ProductController:Controller
    {
        private readonly IBookRepository _repository;
        private readonly ILogger<Product> _logger;

        public ProductController(IBookRepository repository, ILogger<Product> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllProducts());
            }
            catch
            {
                _logger.LogError("Failed");
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody]Product product)
        {
            try
            {
                _repository.AddEntity(product);
                return Created("", product);
            }
            catch
            {
                _logger.LogError("Failed");
                return BadRequest();
            }
        }
    }
}
