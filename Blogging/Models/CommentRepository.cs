using Microsoft.EntityFrameworkCore;

namespace Blogging.Models
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Comment> Comments
        {
            get 
            {  
                return _context.Comments.ToList(); 
            }
        }

        public IEnumerable<Comment> GetCommentsByPostId(int postId)
        {
            return _context.Comments.Where(c => c.PostId == postId).ToList();
        }

        public void CreateComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public Comment GetCommentById(int commentId)
        {
            return _context.Comments.FirstOrDefault(c => c.ID == commentId);
        }

        public void UpdateComment(Comment comment)
        {
            _context.Comments.Update(comment);
            _context.SaveChanges();
        }

        public void DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }
    }
}
