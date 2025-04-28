using Entits;
using DTO;

namespace Services
{
    public interface IOrderServices
    {
        Task<Order> AddOrder(Order order);
        Task<Order> GetOrderById(int id);
    }
}