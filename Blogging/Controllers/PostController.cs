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
            string topic = "Sve Objave";
            IEnumerable<Post> posts;

            var topicObj = _topicRepository.GetTopicById(topicId);
            topic = topicObj.Name;

            if (topic == "Sve Objave")
            {
                posts = _postRepository.Posts.OrderBy(p => p.ID).ToList();
            }
            else
            {
                posts = _postRepository.Posts.Where(p => p.Topic.TopicId == topicId).OrderBy(p => p.ID).ToList();
            }

            return View(new PostListViewModel(posts, topic));
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
            var vm = new CreatePostViewModel();
            ViewBag.Topics = _topicRepository.GetAllTopics();

            return View(vm);
        }

        public IActionResult CreatePost(CreatePostViewModel model)
        {
            var userCookie = Request.Cookies["User"];
            User user = null;

            var post = new Post
            {
                Head = model.Head,
                Body = model.Body,
                Thumbnail = model.Thumbnail,
                TimeCreated = DateTime.Now,
                TimeUpdated = DateTime.Now,
                TopicId = model.TopicId,
                Topic = _topicRepository.GetTopicById(model.TopicId)

            };

            if (userCookie != null)
            {
                user = JsonConvert.DeserializeObject<User>(userCookie);
                post.UserId = user.UserId;
            }
            else
            {
                post.UserId = null;
            }

            var topic = _topicRepository.GetTopicById(model.TopicId);
            post.Topic = topic;

            _postRepository.CreatePost(post);

            _notyf.Success("Objava uspešno postavljena");

            return RedirectToAction("PostList", "Post");
        }
    }
}
