using AspNetCoreHero.ToastNotification.Abstractions;
using Blogging.Models;
using Blogging.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

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

            return View(vm);
        }

        public IActionResult CreatePost(CreatePostViewModel model)
        {
            var userCookie = Request.Cookies["User"];
            var user = JsonConvert.DeserializeObject<User>(userCookie!);

            var post = new Post
            {
                Head = model.Head,
                Body = model.Body,
                Thumbnail = model.Thumbnail,
                TimeCreated = DateTime.Now,
                TimeUpdated = DateTime.Now,
                Topic = model.Topic
            };

            if (user != null)
            {
                post.UserId = user.UserId;
            }

            _postRepository.CreatePost(post);

            _notyf.Success("Objava uspešno postavljena");

            return RedirectToAction("PostList", "Post");
        }
    }
}
