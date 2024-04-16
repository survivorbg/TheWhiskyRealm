using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TheWhiskyRealm.Infrastructure.Data.Validators;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Gets or sets the user birth date.
    /// </summary>
    [Required]
    [PersonalData]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DateOfBirth(MinAge = 18, MaxAge = 120)]
    [Comment("The user birth date.")]
    public DateTime DateOfBirth { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public ICollection<UserEvent> UsersEvents { get; set; } = new List<UserEvent>();
    public ICollection<UserWhisky> UsersWhiskies { get; set; } = new List<UserWhisky>();


}
