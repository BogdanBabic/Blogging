using AspNetCoreHero.ToastNotification.Abstractions;
using Blogging.Models;
using Blogging.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Blogging.Controllers
{
    public class CommentController : Controller
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotyfService _notyf;
        public CommentController(ITopicRepository topicRepository, IPostRepository postRepository, ICommentRepository commentRepository, INotyfService notyf, IUserRepository userRepository)
        {
            _topicRepository = topicRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
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
                TimeCreated = DateTime.Now
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

        [HttpPost]
        public IActionResult RemoveComment(int commentId)
        {
            var loggedInUserName = HttpContext.User.Identity.Name;
            var loggedInUserId = _userRepository.GetUserIdByUsername(loggedInUserName);
            var comment = _commentRepository.GetCommentById(commentId);
            var postId = comment.PostId;

            if (comment.UserId != loggedInUserId)
            {
                _notyf.Error("Ne možete obrisati tuđ komentar");
                return RedirectToAction("ViewPost", "Post", new { postId = postId });
            }

            _commentRepository.DeleteComment(comment);

            _notyf.Success("Komentar uspešno izbrisan");

            return RedirectToAction("ViewPost", "Post", new { postId = postId });
        }
    }
}
