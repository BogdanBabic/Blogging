using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogging.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Korisničko ime")]
        [StringLength(15, ErrorMessage = "Korisničko ime može da sadrži maksimum 15 karaktera")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Šifra je obavezna")]
        [DataType(DataType.Password)]
        [Display(Name = "Šifra")]
        [RegularExpression("^(?=(.*[a-z]){1,})(?=(.*[A-Z]){1,})(?=(.*[0-9]){1,})(?=(.*[!@#$%^&*()\\-__+.]){1,}).{8,}$",
            ErrorMessage = "Šifra mora imati najmanje 1 malo i veliko slovo, broj i specijalni karakter")]
        [StringLength(20, ErrorMessage = "Šifra može da sadrži maksimum 30 karaktera")]
        public string Password { get; set; }

        [NotMapped]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Šifre se ne poklapaju")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "Neadekvatan Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Ime")]
        [StringLength(10, ErrorMessage = "Ime može da sadrži maksimum 15 karaktera")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Prezime")]
        [StringLength(30, ErrorMessage = "Prezime može da sadrži maksimum 15 karaktera")]
        public string LastName { get; set; }
    }
}
