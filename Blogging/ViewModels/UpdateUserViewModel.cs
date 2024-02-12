using System.ComponentModel.DataAnnotations;

namespace Blogging.ViewModels
{
    public class UpdateUserViewModel
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Korisničko ime")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Trenutna šifra")]
        public string CurrentPassword { get; set; }

        [Required]
        [RegularExpression("^(?=(.*[a-z]){1,})(?=(.*[A-Z]){1,})(?=(.*[0-9]){1,})(?=(.*[!@#$%^&*()\\-__+.]){1,}).{8,15}$",
                            ErrorMessage = "Nova šifra mora imati najmanje 1 veliki karakter, 1 mali karakter, broj, specijalni karakter i dužine 8-15 karaktera")]
        [Display(Name = "Nova šifra")]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Potvrdi novu šifru")]
        [Compare("NewPassword", ErrorMessage = "Uneta šifra se ne poklapa!")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "Uneti Email je pogrešan")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Ime")]
        [StringLength(10, ErrorMessage = "Ime može da sadrži maksimum 15 karaktera")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Prezime")]
        [StringLength(20, ErrorMessage = "Prezime može da sadrži maksimum 15 karaktera")]
        public string LastName { get; set; }
    }
}