using Entits;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        public static List<User> Users { get; set; }

        AdoNetManageContext _AdoNetManageContext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(AdoNetManageContext manageDbContext, ILogger<UserRepository> logger) 
        {
            this._AdoNetManageContext = manageDbContext;
            _logger = logger;
        }

    //public UserRepository(AdoNetManageContext manageDbContext)
    //    {
    //        this._AdoNetManageContext = manageDbContext;

    //    }

    public async Task<User> AddUser(User user)
        {
            //int numberOfUsers = System.IO.File.ReadLines("M:\\webAPI\\OurShop\\OurShop\\Users.txt").Count();
            //user.Id = numberOfUsers + 1;
            //string userJson = JsonSerializer.Serialize(user);
            //System.IO.File.AppendAllText("M:\\webAPI\\OurShop\\OurShop\\Users.txt", userJson + Environment.NewLine);
            //return (user);

            //return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            await _AdoNetManageContext.Users.AddAsync(user);
            await _AdoNetManageContext.SaveChangesAsync();
            return user;

        }



        public async Task<User> Login(string email, string password)
        {
            User user = await _AdoNetManageContext.Users.FirstOrDefaultAsync(u => (u.Email == email && u.Password == password));
          

            if (user != null)
                _logger.LogInformation($"login attempted with User Name , {email} and password{password}");

            return user;

            //using (StreamReader reader = System.IO.File.OpenText("M:\\webAPI\\OurShop\\OurShop\\Users.txt"))
            //{
            //    string? currentUserInFile;
            //    while ((currentUserInFile = reader.ReadLine()) != null)
            //    {
            //        User user = JsonSerializer.Deserialize<User>(currentUserInFile);
            //        if (user.Email == email && user.Password == password)

            //            return user;


            //    }
            //}
            //return null;


        }

        public async Task<User> GetUserById(int id)
        {
            User user = await _AdoNetManageContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;

        }




        public async Task UpdateUser(int id, User userToUpdate)
        {
            userToUpdate.Id = id;

            _AdoNetManageContext.Users.Update(userToUpdate);

            await _AdoNetManageContext.SaveChangesAsync();

            //string textToReplace = string.Empty;
            //using (StreamReader reader = System.IO.File.OpenText("M:\\webAPI\\OurShop\\OurShop\\Users.txt"))
            //{
            //    string currentUserInFile;
            //    while ((currentUserInFile = reader.ReadLine()) != null)
            //    {

            //        User user = JsonSerializer.Deserialize<User>(currentUserInFile);
            //        if (user.Id == id)
            //            textToReplace = currentUserInFile;
            //    }
            //}

            //if (textToReplace != string.Empty)
            //{
            //    string text = System.IO.File.ReadAllText("M:\\webAPI\\OurShop\\OurShop\\Users.txt");
            //    text = text.Replace(textToReplace, JsonSerializer.Serialize(userToUpdate));
            //    System.IO.File.WriteAllText("M:\\webAPI\\OurShop\\OurShop\\Users.txt", text);
            //}




        }








    }
}
