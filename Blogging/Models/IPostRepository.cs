namespace Blogging.Models
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAllPosts();
        List<Post> GetPostsByTopicId(int? topicId);
        Post GetPostById(int postId);
        void CreatePost(Post post);
        void DeletePost(Post post);
        void UpdatePost(Post post);
    }

}
