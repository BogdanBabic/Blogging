namespace Blogging.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Head { get; set; }
        public string Body { get; set; }
        public string Thumbnail { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; } = DateTime.Now;
        public Topic Topic { get; set; }
        public int? UserId { get; set; }
    }
}
