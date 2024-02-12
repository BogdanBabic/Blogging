using AspNetCoreHero.ToastNotification.Abstractions;
using Blogging.Models;
using System.ComponentModel.DataAnnotations;

namespace Blogging.ViewModels;

public class CreateCommentViewModel
{
    public int ID { get; set; }

    [Required(ErrorMessage = "Unesite sadržaj komentara")]
    [StringLength(255, ErrorMessage = "Prekoračili ste limit karaktera (maksimalno 255)")]
    public string Body { get; set; }
    public DateTime TimeCreated { get; set; } = DateTime.Now;
    public int? UserId { get; set; }
    public int PostId { get; set; }
}
