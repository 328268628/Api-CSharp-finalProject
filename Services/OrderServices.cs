using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entits;
using Repository;
using DTO;
using Microsoft.Extensions.Logging;
namespace Services
{
    public class OrderServices : IOrderServices
    {
        private readonly ILogger<OrderServices> _logger;
        IOrderRepository orderRepository;
        IProductRepository productRepository;

        public OrderServices(IOrderRepository orderRepository, IProductRepository productRepository, ILogger<OrderServices> logger)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Order> AddOrder(Order order)
        {
            order.OrderDate = DateTime.Now;
            Order order1 = await getSum(order);
            return await orderRepository.AddOrder(order1);

        }

        public async Task<Order> GetOrderById(int id)
        {
            return await orderRepository.GetOrderById(id);
        }



        private async Task<Order> getSum(Order Order)
        {
            float sum = 0;
            foreach (var product in Order.OrderItems)
            {
                Product goodProduct = await productRepository.GetById(product.ProductId);
                sum += (float)goodProduct.Price;
            }
            if (Order.OrderSum != sum)
            {

                Order.OrderSum = sum;
                _logger.LogCritical("הכניס סכום בכוחות עצמו" + Order.UserId + "משתמש ");
            }

            return Order;
        }



      

    }
}
