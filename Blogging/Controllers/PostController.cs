using AspNetCoreHero.ToastNotification.Abstractions;
using Blogging.Models;
using Blogging.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Blogging.Controllers
{
    public class PostController : Controller
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IPostRepository _postRepository;
        private readonly INotyfService _notyf;
        public PostController(ITopicRepository topicRepository, IPostRepository postRepository, INotyfService notyf)
        {
            _topicRepository = topicRepository;
            _postRepository = postRepository;
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

        public IActionResult ViewPost(int id)
        {
            Post p = _postRepository.GetPostById(id);

            return View(p);
        }

        public IActionResult PostCreator()
        {
            var vm = new CreatePostViewModel();

            return View(vm);
        }

        public IActionResult CreatePost(CreatePostViewModel model)
        {
            if (Request.HttpContext.User.Identity.IsAuthenticated)
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
                    Topic = model.Topic,
                    UserId = user.UserId
                };

                _postRepository.CreatePost(post);

                _notyf.Success("Objava uspešno kreirana");

                return RedirectToAction("PostList", "Post");
            }

            var anonymousPost = new Post
            {
                Head = model.Head,
                Body = model.Body,
                Thumbnail = model.Thumbnail,
                TimeCreated = DateTime.Now,
                TimeUpdated = DateTime.Now,
                Topic = model.Topic
            };

            _postRepository.CreatePost(anonymousPost);

            _notyf.Success("Objava uspešno kreirana");

            return RedirectToAction("PostList", "Post");
        }
    }
}
