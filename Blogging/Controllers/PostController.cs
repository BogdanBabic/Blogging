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
                posts = _postRepository.GetPostsByTopicId(topicId).OrderByDescending(p => p.TimeCreated);
            }
            else
            {
                posts = _postRepository.GetAllPosts().OrderByDescending(p => p.TimeCreated);
            }

            return View(new PostListViewModel(posts, topicName));
        }


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
            };

            if (user != null)
            {
                post.UserId = user.UserId;
            }
            else
            {
                post.UserId = null;
            }
            _postRepository.CreatePost(post);
            _notyf.Success("Objava uspešno postavljena");

            return RedirectToAction("PostList");
        }

        public IActionResult RemovePost(int postId)
        {
            var loggedInUserName = HttpContext.User.Identity.Name;
            var logegdInUserId = _userRepository.GetUserIdByUsername(loggedInUserName);
            var post = _postRepository.GetPostById(postId);


            if (postId != null)
            {
                _postRepository.DeletePost(post);
            }

            _notyf.Success("Objava uspešno obrisana");

            return RedirectToAction("PostList");

        }

        public IActionResult PostEditor(int postId)
        {
            var post = _postRepository.GetPostById(postId);
            ViewBag.Topics = _topicRepository.GetAllTopics() ?? new List<Topic>();

            UpdatePostViewmodel vm = new UpdatePostViewmodel()
            {
                ID = post.ID,
                Head = post.Head,
                Body = post.Body
            };

            return View(vm);
        }

        public IActionResult EditPost(UpdatePostViewmodel model)
        {
            var post = _postRepository.GetPostById(model.ID);

            post.Head = model.Head;
            post.Body = model.Body;
            post.ID = model.ID;
            post.TopicId = model.TopicId;

            _notyf.Success("Objava izmenjena");
            _postRepository.UpdatePost(post);

            return View("ViewPost", post);
        }

    }
}
