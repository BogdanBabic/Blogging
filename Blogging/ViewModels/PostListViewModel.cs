using Blogging.Models;

namespace Blogging.ViewModels
{
    public class PostListViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public string? SelectedTopic { get; set; }

        public PostListViewModel(IEnumerable<Post> posts, string? topic)
        {
            Posts = posts;
            SelectedTopic = topic;
        }
    }
}
