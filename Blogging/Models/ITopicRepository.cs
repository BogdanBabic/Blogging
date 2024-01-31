namespace Blogging.Models
{
    public interface ITopicRepository
    {
        IEnumerable<Topic> GetAllTopics();

        Topic GetTopicById(int? id);
    }
}
