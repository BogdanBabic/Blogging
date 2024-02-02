using Blogging.Models;

namespace Blogging.ViewModels
{
    public class TopicListViewModel
    {
        public IEnumerable<Topic> Topics { get; set; }
        public TopicListViewModel(IEnumerable<Topic> topics)
        {
            Topics = topics;
        }
    }
}
