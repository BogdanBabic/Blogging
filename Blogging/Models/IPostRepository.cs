namespace Blogging.Models
{
    public interface IPostRepository
    {
        IEnumerable<Post> Posts { get; }
        Post GetPostById(int id);
        void CreatePost(Post post);
    }
}
