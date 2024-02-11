namespace Blogging.Models
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetCommentsByPostId(int postId);
        void CreateComment(Comment comment);
    }
}
