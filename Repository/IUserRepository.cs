using Entits;

namespace Repository
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User> GetUserById(int id);
        Task<User> Login(string email, string password);
        Task UpdateUser(int id, User userToUpdate);
    }
}