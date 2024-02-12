using System.ComponentModel.DataAnnotations.Schema;

namespace Blogging.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string Body { get; set; }
        public DateTime TimeCreated {  get; set; } = DateTime.Now;
    }
}
