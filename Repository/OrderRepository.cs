using Entits;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Entits;


using AutoMapper;
using Microsoft.Extensions.Logging;
namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
  
        AdoNetManageContext _AdoNetManageContext;
        IMapper mapper;
        public OrderRepository(AdoNetManageContext manageDbContext, IMapper mapper)
        {
            this._AdoNetManageContext = manageDbContext;
            this.mapper = mapper;
        }

        public async Task<Order> AddOrder(Order order)
        {
            
            await _AdoNetManageContext.Orders.AddAsync(order);
            await _AdoNetManageContext.SaveChangesAsync();
            return order;                    

        }

        public async Task<Order> GetOrderById(int id)
        {
            //Include(o => o.OrderItems)
            Order o = await _AdoNetManageContext.Orders.Include(p => p.User).Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);
            return o;

        }

       

    }
}
