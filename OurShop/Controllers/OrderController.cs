using Microsoft.AspNetCore.Mvc;
using Services;
using Entits;
using DTO;
using AutoMapper;
using System.Reflection.Metadata.Ecma335;
using MailKit.Security;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OurShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        IOrderServices orderServices;
        IMapper mapper;
        public OrderController(IOrderServices orderServices, IMapper mapper)
        {
            this.orderServices = orderServices;
            this.mapper = mapper;
        }

        // GET: api/<OrderController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<OrderDTO> GetOrderById(int id)
        {
            Order order = await orderServices.GetOrderById(id);
            OrderDTO orderDTOs = mapper.Map<Order, OrderDTO>(order);
            return orderDTOs;
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult<OrderDTO> >Post( OrderPostDTO order)
        {
            Order order1 =mapper.Map<OrderPostDTO, Order>(order);

            Order order2 =await orderServices.AddOrder(order1);
           OrderDTO order3= mapper.Map<Order, OrderDTO>(order2);
          return order3 == null? BadRequest():CreatedAtAction(nameof(GetOrderById), new {id = order3.Id},order3);

    }

        // PUT api/<OrderController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<OrderController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
