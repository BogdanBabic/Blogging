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


        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts.Include(p => p.Topic).ToList();
        }

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public List<Post> GetPostsByTopicId(int? topicId)
        {
            return _context.Posts.Include(p => p.Topic).Where(p => p.TopicId == topicId).OrderBy(p => p.ID).ToList();
        }
    }
}
