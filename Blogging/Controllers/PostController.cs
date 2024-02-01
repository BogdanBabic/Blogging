using Blogging.Models;
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
        public ViewResult PostList(int? topicId)
        {
            IEnumerable<Post> posts;
            string? topic = "";

            var topicObj = _topicRepository.GetTopicById(topicId);

            if (topicObj != null)
            {
                topic = topicObj.Name;
            }

            return View();
        }
    }
}
