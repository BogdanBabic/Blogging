using Blogging.Helpers;

namespace Blogging.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            user.Password = EncryptionHelper.Encrypt(user.Password);

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username)!;
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }

        public string GetUsernameByUserId(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            return user != null ? user.Username : null;
        }

        public void UpdatePassword(User user, string newPassword)
        {
            user.Password = EncryptionHelper.Encrypt(newPassword);

            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
