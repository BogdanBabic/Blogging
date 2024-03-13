using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Blogging.Models;

namespace Blogging.Components.TopicMenu
{
    public class TopicMenu : ViewComponent
    {
        private readonly ITopicRepository _topicRepository;

        public TopicMenu(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public IViewComponentResult Invoke()
        {
            var topics = _topicRepository.GetAllTopics().OrderBy(t => t.Name).ToList();

            //var userCookie = HttpContext!.Request.Cookies["User"];

            //if (userCookie != null)
            //{
            //    var topicToRemove = topics.Single(t => t.Name == "Moje Objave");
            //    topics.Remove(topicToRemove);
            //}

            return View(topics);
        }
    }
}
