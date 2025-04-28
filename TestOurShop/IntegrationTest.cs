using Entits;
using Repository;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOurShop
{
    public class IntegrationTest:IClassFixture<DatabaseFixture>
    {
        private readonly AdoNetManageContext _context;

        public IntegrationTest(DatabaseFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task DB_Login_ReturnsOkResult_WithValidUser()
        {
            // Arrange
            var user = new User { Email = "batya@example.com", Password = "b0583272120!!!" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var userRepository = new UserRepository(_context);


            // Act
            var retrievedUser = await userRepository.Login(user.Email, user.Password);

            // Assert
            Assert.NotNull(retrievedUser);
            Assert.Equal(user.Email, retrievedUser.Email);
            Assert.Equal(user.Password, retrievedUser.Password);
        }

        [Fact]
        public async Task DB_Login_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            var userRepository = new UserRepository(_context);

            // Act
            var result = await userRepository.Login("wrongPassword", "nonExistentUser");

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public async Task DB_LogIn_WrongPassword_ReturnsNull()
        {
            // Arrange
            var user = new User { Email = "BatyaOzeri.com", Password = "batya3272120@gmail" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userRepository = new UserRepository(_context);

            // Act
            var result = await userRepository.Login("wrongPassword", user.Email);

            // Assert
            Assert.Null(result);
        }

        //[Fact]
        //public async Task DB_CreateOrderSum_CHeckOrderSum_ReturnsOrder()
        //{
        //    // Arrange
        //    _context.Categories.Add(new Category { Name = "basic" });
        //    await _context.SaveChangesAsync();

        //    List<Product> products = new List<Product> { new() { Price = 6, Name = "Milk", Id = 1, Description = "tasty", Image = "1.jpg" }, new() { Price = 20, Name = "eggs", Id = 1, Description = "tasty", Image = "1.jpg" } };
        //    _context.Products.AddRange(products);
        //    await _context.SaveChangesAsync();

        //    var orderItems = new List<OrderItem>() { new() { ProductId = 1 }, new() { ProductId = 2 } };
        //    var order = new Order { OrderSum = 26, OrderItems = orderItems };

        //    var orderService = new OrderServices(new IOrderRepository(_context));

        //    // Act
        //    var result = await orderService.AddOrder(order);

        //    // Assert
        //    Assert.Equal(order, result);
        //}

        //[Fact]
        //public async Task CreateOrderSum_CHeckOrderSum_ReturnsNull()
        //{
        //    // Arrange
        //    _context.Categories.Add(new Category {Name = "basic" });
        //    await _context.SaveChangesAsync();

        //    List<Product> products = new() { new() { Price = 6, Name = "Milk",Id = 1, Description = "tasty", Image = "1.jpg" }, new() { Price = 20, Name = "eggs", Id = 1, Description = "tasty", Image = "1.jpg" } };
        //    _context.Products.AddRange(products);
        //    await _context.SaveChangesAsync();

        //    var orderItems = new List<OrderItem>() { new() { ProductId = 1 }, new() { ProductId = 2 } };
        //    var order = new Order { OrderSum = 50, OrderItems = orderItems };

        //    var orderService = new OrderServices(new OrderRepository(_context), new ProductsRepository(_context));

        //    // Act
        //    var result = await orderService.AddOrder(order);

        //    // Assert
        //    Assert.Null(result);
        //}





    }
}
