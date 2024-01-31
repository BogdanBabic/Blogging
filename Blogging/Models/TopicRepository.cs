namespace Blogging.Models
{
    public class TopicRepository : ITopicRepository
    {
        private readonly ApplicationDbContext _context;

        public TopicRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Topic> GetAllTopics()
        {
            return _context.Topics;
        }

        public Topic GetTopicById(int? id)
        {
            return _context.Topics.FirstOrDefault(t => t.ID == id)!;
        }
    }
}