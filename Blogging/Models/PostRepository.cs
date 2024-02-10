using Microsoft.EntityFrameworkCore;
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
            return _context.Posts.Include(p => p.Topic).FirstOrDefault(p => p.ID == id)!;
        }

        public IEnumerable<Post> Posts
        {
            get
            {
                return _context.Posts.Include(p => p.Topic);
            }
        }

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
    }
}
