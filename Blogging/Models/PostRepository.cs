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
            return _context.Posts.Include(p => p.Topic).Include(p => p.User).FirstOrDefault(p => p.ID == id)!;
        }


        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts.Include(p => p.Topic).ToList();
        }

        public List<Post> GetPostsByTopicId(int? topicId)
        {
            return _context.Posts.Include(p => p.Topic).Where(p => p.TopicId == topicId).OrderBy(p => p.ID).ToList();
        }

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void DeletePost(Post post)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges(true);
        }
    }
}
