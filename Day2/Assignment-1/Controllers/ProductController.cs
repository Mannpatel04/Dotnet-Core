using Assignment_1.Interfaces;
using Assignment_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Assignment_1.Controllers
{

    [Route("api/products")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
           return Ok(_service.GetAll());
        }
           

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var product = _service.GetById(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpGet("category/{name}")]
        public IActionResult GetByCategory(string name)
        {
             return Ok(_service.GetByCategory(name));
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _service.Add(product);
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return _service.Delete(id) ? NoContent() : NotFound();
        }   
    }


}

