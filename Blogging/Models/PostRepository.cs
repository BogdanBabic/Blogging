using System.Linq;
namespace Blogging.Models
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Post GetPostById(int id)
        {
            return _context.Posts.Include(p => p.)
        }
    }
}
