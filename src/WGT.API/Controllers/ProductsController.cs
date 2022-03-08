using Microsoft.AspNetCore.Mvc;
using WGT.API.Models;
using WGT.API.Repositories;

namespace WGT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }
        
        // GET: api/<ProductsController>
        [HttpGet]
        public Task<IReadOnlyList<Product>> Get()
        {
            return _repository.GetProductListAsync();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductsController>
        [HttpPost]
        public Task Post([FromBody] Product product)
        {
            return _repository.CreateProductAsync(product);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product value)
        {
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
