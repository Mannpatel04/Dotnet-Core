using Assignment_1.DTOs;
using Assignment_1.Interfaces;
using Assignment_1.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Assignment_1.Controllers
{
  
    [Route("api/products")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;


        public ProductController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

     
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _service.GetAll();
            var result = _mapper.Map<List<ProductReadDTO>>(products);
            return Ok(result);
        }

      
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var product = _service.GetById(id);
            if (product == null) return NotFound();

            return Ok(_mapper.Map<ProductReadDTO>(product));
        }

       
        [HttpGet("category/{name}")]
        public IActionResult GetByCategory(string name)
        {
             return Ok(_service.GetByCategory(name));
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult Create(ProductCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<Product>(dto);
            _service.Add(product);

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.Id },
                _mapper.Map<ProductReadDTO>(product)
            );
        }

        [Authorize(Roles= "admin")]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return _service.Delete(id) ? NoContent() : NotFound();
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, ProductUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<Product>(dto);
            product.Id = id;

            if (!_service.Update(product))
                return NotFound();

            return NoContent();
        }
    }


}

