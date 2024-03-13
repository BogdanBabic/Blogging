namespace Blogging.Models
{
    public interface IPostRepository
    {
        //IEnumerable<Post> Posts {  get; }
        IEnumerable<Post> GetAllPosts();
        List<Post> GetPostsByTopicId(int? topicId);
        Post GetPostById(int postId);
        void CreatePost(Post post);
        //void UpdatePost(Post post);
        //void DeletePost(int postId);
    }

}
