using AutoMapper;
using Entits;
using DTO;

namespace OurShop
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Product, ProductDTO>().ForMember(c => c.CategoryName, name => name.MapFrom(src => src.Category.Name));
            
            CreateMap<User, UserDTO>();

            CreateMap<Order, OrderDTO>();

            CreateMap<OrderDTO, Order>();

            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();

            CreateMap<OrderPostDTO,Order >();

           

        }
    }
}
