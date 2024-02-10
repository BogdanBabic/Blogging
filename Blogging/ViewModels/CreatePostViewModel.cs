using Blogging.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blogging.ViewModels
{
    public class CreatePostViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Naslov je obavezan")]
        [Display(Name = "Naslov")]
        [StringLength(30, ErrorMessage = "Predugačak naslov (maksimalno 30 karaktera)")]
        public string Head { get; set; }

        [Required(ErrorMessage = "Unesite tekst objave")]
        [Display(Name = "Šta vam je na umu?")]
        [StringLength(500, ErrorMessage = "Prekoračili ste limit karaktera (maksimalno 500)")]
        public string Body { get; set; }

        [Display(Name = "Slika")]
        [StringLength(255, ErrorMessage = "Predugačak URL")]
        public string Thumbnail { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.Now;
        public DateTime TimeUpdated { get; set; } = DateTime.Now;
        public int TopicId { get; set; }

        [Required(ErrorMessage = "Izaberite kategoriju objave")]
        [Display(Name = "Tema / Kategorija")]
        public Topic Topic { get; set; }
        public int? UserId { get; set; }
    }
}
