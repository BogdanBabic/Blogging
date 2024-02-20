namespace Blogging.Models
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        User GetUserByUsername(string username);
        User GetUserById(int id);
        string GetUsernameByUserId(int userId);
        int? GetUserIdByUsername(string username);
        void UpdatePassword(User user, string newPassword);
    }
}
