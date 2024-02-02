using Blogging.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blogging.Controllers
{
    public class TopicController : Controller
    {
        public readonly ITopicRepository _topicRepository;
        public readonly IPostRepository _postRepository;
        public TopicController(ITopicRepository topicRepository, IPostRepository postRepository)
        {
            _topicRepository = topicRepository;
            _postRepository = postRepository;
        }

        public IActionResult TopicList()
        {
            IEnumerable<Topic> topics = _topicRepository.GetAllTopics();
            return View(topics);
        }
    }
}
