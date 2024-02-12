namespace Blogging.Models
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetCommentsByPostId(int postId);
        void CreateComment(Comment comment);
        Comment GetCommentById(int commentId);
        void UpdateComment(Comment comment);
        void DeleteComment(Comment comment);
    }
}
