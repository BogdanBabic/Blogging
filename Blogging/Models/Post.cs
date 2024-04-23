using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogging.Models
{
    public class Post
    {
        [BindNever]
        public int ID { get; set; }
        public string Head { get; set; }
        public string Body { get; set; }
        public string? Thumbnail { get; set; }

        [BindNever]
        public DateTime TimeCreated { get; set; } = DateTime.Now;

        [BindNever]
        public DateTime TimeUpdated { get; set; } = DateTime.Now;
        public int? TopicId { get; set; }
        public Topic? Topic { get; set; }
        public int? UserId { get; set; }
        
        [ForeignKey("UserId")]
        public User? User { get; set; }

        [NotMapped]
        public IEnumerable<Comment> Comments { get; set; }
    }
}
