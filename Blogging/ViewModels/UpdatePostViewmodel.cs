using Blogging.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Blogging.ViewModels
{
    public class UpdatePostViewmodel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Naslov je obavezan")]
        [Display(Name = "Izmenite Naslov")]
        [StringLength(30, ErrorMessage = "Predugačak naslov (maksimalno 30 karaktera)")]
        public string Head { get; set; }

        [Required(ErrorMessage = "Unesite tekst objave")]
        [Display(Name = "Izmenite sadržaj objave")]
        [StringLength(500, ErrorMessage = "Prekoračili ste limit karaktera (maksimalno 500)")]
        public string Body { get; set; }

        [Display(Name = "Slika")]
        [StringLength(255, ErrorMessage = "Predugačak URL")]
        public string Thumbnail { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.Now;
        public DateTime TimeUpdated { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Izaberite drugu kategoriju objave")]
        [Display(Name = "Tema / Kategorija")]
        public int TopicId { get; set; }
        public IEnumerable<Topic> Topics { get; set; }

    }
}
