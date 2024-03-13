using AspNetCoreHero.ToastNotification.Abstractions;
using Blogging.Models;
using Blogging.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Blogging.Controllers
{
    public class PostController : Controller
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly INotyfService _notyf;
        public PostController(ITopicRepository topicRepository, IPostRepository postRepository, ICommentRepository commentRepository, INotyfService notyf, IUserRepository userRepository)
        {
            _topicRepository = topicRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _notyf = notyf;
        }
        public ViewResult PostList(int? topicId)
        {
            IEnumerable<Post> posts;
            string topicName = "Sve Objave";

            if (topicId.HasValue)
            {
                var topicObj = _topicRepository.GetTopicById(topicId.Value);
                topicName = topicObj.Name;
                posts = _postRepository.GetPostsByTopicId(topicId);
            }
            else
            {
                posts = _postRepository.GetAllPosts();
            }

            return View(new PostListViewModel(posts, topicName));
        }


        [HttpPost]
        public IActionResult ViewPost(int postId)
        {
            Post p = _postRepository.GetPostById(postId);

            if (p == null)
            {
                return NotFound();
            }

            var comments = _commentRepository.GetCommentsByPostId(postId);

            foreach (var comment in comments)
            {
                if (comment.UserId != null)
                {
                    comment.User = _userRepository.GetUserById(comment.UserId.Value);
                }
            }

            p.Comments = comments;

            return View(p);
        }


        public IActionResult PostCreator()
        {
            ViewBag.Topics = _topicRepository.GetAllTopics() ?? new List<Topic>();
            return View(new CreatePostViewModel());
        }

        [HttpPost]
        public IActionResult CreatePost(CreatePostViewModel model)
        {
           
            var userCookie = Request.Cookies["User"];
            User user = null;

            if (userCookie != null)
            {
                user = JsonConvert.DeserializeObject<User>(userCookie);
            }

            var post = new Post
            {
                Head = model.Head,
                Body = model.Body,
                Thumbnail = model.Thumbnail,
                TimeCreated = DateTime.Now,
                TimeUpdated = DateTime.Now,
                TopicId = model.TopicId,
                UserId = user?.UserId
            };

            _postRepository.CreatePost(post);
            _notyf.Success("Objava uspešno postavljena");

            //if (!ModelState.IsValid)
            //{
            //    ViewBag.Topics = _topicRepository.GetAllTopics() ?? new List<Topic>(); // Repopulate topics on validation fail
            //    return View("PostCreator", model);
            //}


            return RedirectToAction("PostList");
        }

    }
}
