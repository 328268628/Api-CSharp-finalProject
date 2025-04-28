using Entits;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;
using Services;
namespace TestOurShop
{
    public class UnitTest1
    {
        [Fact]
        private async Task Login_ReturnsOkResult_WithValidUser()
        {
            var user = new User() { Email = "bb@gmail.com", Password = "bo0583272120" };

            var mockContext = new Mock<AdoNetManageContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new UserRepository(mockContext.Object);

            var result = await userRepository.Login(user.Email, user.Password);

            Assert.Equal(user, result);



        }

        [Fact]
        public async Task Login_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            var mockContext = new Mock<AdoNetManageContext>();
            var users = new List<User>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
                     
            var userRepository = new UserRepository(mockContext.Object);
            // Act
            var result = await userRepository.Login("test@example.com", "test@example.com");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task LogIn_WrongPassword_ReturnsNull()
        {
            // Arrange
            var user = new User { Email = "BatyaOzeri", Password = "batya!!@gmail.com" };
            var mocContext = new Mock<AdoNetManageContext>();
            var users = new List<User> { user };
            mocContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mocContext.Object);

            // Act
            var result = await userRepository.Login(user.Email, "wrongPassword");

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public async Task CreateOrderSum_CHeckOrderSum_ReturnsOrder()
        {
            // Arrange
            var orderItems = new List<OrderItem>() { new() { ProductId = 1, Quantity = 1 } };
            var order = new Order { OrderSum = 50, OrderItems = orderItems };

            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(x => x.AddOrder(It.IsAny<Order>())).ReturnsAsync(order);

            List<Product> products = new List<Product> { new() { Id = 1, Price = 50 } };
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(x => x.GetProduct(It.IsAny<int>(), It.IsAny<int>(),It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int?[]>()))
                                 .ReturnsAsync(products);
            var orderService = new OrderServices(mockOrderRepository.Object);

            // Act
            var result = await orderService.AddOrder(order);

            // Assert
            Assert.Equal(order, result);
        }


        [Fact]
        public async Task AddOrder_WhenOrderIsNull_ReturnsNull()
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(x => x.AddOrder(It.IsAny<Order>())).ReturnsAsync((Order)null);

            var orderService = new OrderServices(mockOrderRepository.Object);

            // Act
            var result = await orderService.AddOrder(null);

            // Assert
            Assert.Null(result); // Ensure the result is null
        }
    }
}