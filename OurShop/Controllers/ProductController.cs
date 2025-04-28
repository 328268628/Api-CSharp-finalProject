using AutoMapper;
using DTO;
using Entits;
using Microsoft.AspNetCore.Mvc;
using Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OurShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductServices productServices;
        IMapper mapper;

        public ProductController(IProductServices productServices,IMapper mapper)
        {
            this.productServices = productServices;
            this.mapper = mapper;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<List<ProductDTO>> Get([FromQuery] int position, [FromQuery] int skip, [FromQuery] string? desc, [FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] int?[] categoryIds)
        { 
            
             List <Product> productList = await productServices.GetProduct(position, skip, desc, minPrice, maxPrice, categoryIds);
             List<ProductDTO> productDTOs = mapper.Map<List<Product>, List<ProductDTO>>(productList);
             return productDTOs;
            

        }

        // GET api/<ProductController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ProductController>
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //    //return await productServices.AddProduct(product);
        //}

        // PUT api/<ProductController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] Product productToUpdate)
        //{
        //    //await productServices.UpdateProduct(id, productToUpdate);
        //}

        // DELETE api/<ProductController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id, [FromBody] Product productToDelete)
        //{
        //    //await productServices.DeleteProduct(id, productToDelete);
        //}
    }
}
