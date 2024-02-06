using Blogging.Models;
using Blogging.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blogging.Controllers
{
    public class PostController : Controller
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IPostRepository _postRepository;
        public PostController(ITopicRepository topicRepository, IPostRepository postRepository)
        {
            _topicRepository = topicRepository;
            _postRepository = postRepository;

        }
        public ViewResult PostList(int topicId)
        {
            IEnumerable<Post> posts;
            string? topic = "Sve Objave";

            var topicObj = _topicRepository.GetTopicById(topicId);

            if (topicObj != null)
            {
                topic = topicObj.Name;
                posts = _postRepository.Posts.Where(p => p.Topic.TopicId == topicId).OrderBy(p => p.ID).ToList();
            }

            if (topic == "Sve Objave")
            {
                posts = _postRepository.Posts.OrderBy(p => p.ID).ToList();
            }

            else
            {
                posts = _postRepository.Posts.ToList();
            }

            return View(new PostListViewModel(posts, topic));
        }
    }
}
