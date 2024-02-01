using Blogging.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blogging.Controllers
{
    public class PostController : Controller
    {
        private readonly ITopicRepository _topicRepository;
        public PostController()
        {
            
        }
        public IActionResult PostList()
        {
            return View();
        }
    }
}
