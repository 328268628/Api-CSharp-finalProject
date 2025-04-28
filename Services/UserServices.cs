using System.ComponentModel.DataAnnotations;
using System.Text.Json;

using Entits;
using Repository;
using Zxcvbn;


namespace Services
{
    public class UserServices : IUserServices
    {
        IUserRepository userRepository;

        public UserServices(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> AddUser(User user)
        {
            int a = user.Email.IndexOf('@');
            int b = user.Email.IndexOf(".com");
          
            if (a != -1 && b != -1 && a < b)
            {
                return await userRepository.AddUser(user);
            }
            else
            {
                return (null);
            }

        }

        public int cheakPassword(string password)
        {

            var result = Zxcvbn.Core.EvaluatePassword(password);

            return result.Score;

        }

        public async Task<User> Login(string email, string password)
        {
            return await userRepository.Login(email, password);


        }

        public async Task<User> GetUserById(int id)
        {
            return await userRepository.GetUserById(id);
        }



        public async Task UpdateUser(int id, User userToUpdate)
        {

            await userRepository.UpdateUser(id, userToUpdate);

        }

    }
}
