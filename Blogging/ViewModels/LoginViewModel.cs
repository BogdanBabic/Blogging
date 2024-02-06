using System.ComponentModel.DataAnnotations;

namespace Blogging.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Korisničko ime")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Šifra")]
        public string Password { get; set; }
    }
}
