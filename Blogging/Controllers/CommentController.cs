using AspNetCoreHero.ToastNotification.Abstractions;
using Blogging.Models;
using Blogging.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Blogging.Controllers
{
    public class CommentController : Controller
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly INotyfService _notyf;
        public CommentController(ITopicRepository topicRepository, IPostRepository postRepository, ICommentRepository commentRepository, INotyfService notyf)
        {
            _topicRepository = topicRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _notyf = notyf;
        }

        [HttpPost]
        public IActionResult CreateComment(int postId, string body)
        {
            var userCookie = Request.Cookies["User"];
            User user = null;

            if (userCookie != null)
            {
                user = JsonConvert.DeserializeObject<User>(userCookie);
            }

            var comment = new Comment
            {
                PostId = postId,
                Body = body,
                TimeCreated = DateTime.Now,
                TimeUpdated = DateTime.Now
            };

            if (user != null)
            {
                comment.UserId = user.UserId;
            }
            else
            {
                comment.UserId = null;
            }

            _commentRepository.CreateComment(comment);

            _notyf.Success("Komentar uspešno postavljen");

            return RedirectToAction("ViewPost", "Post", new { postId = postId });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
