using Entits;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Zxcvbn;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public class UserServices : IUserServices
    {
        IUserRepository userRepository;
        private readonly IConfiguration _configuration;

        public UserServices(IUserRepository userRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            _configuration = configuration;
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

        public async Task<(User user, string token)> Login(string email, string password)
        {
            User user=await userRepository.Login(email, password);
            var secretKey = _configuration["Jwt:Key"];
            var token = GenerateJwtToken(user, secretKey);
            return (user, token);


        }

        public async Task<User> GetUserById(int id)
        {
            return await userRepository.GetUserById(id);
        }



        public async Task UpdateUser(int id, User userToUpdate)
        {

            await userRepository.UpdateUser(id, userToUpdate);

        }
        public string GenerateJwtToken(User user, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.FirstName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
