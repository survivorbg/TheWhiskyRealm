using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TheWhiskyRealm.Infrastructure.Data.Validators;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [PersonalData]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DateOfBirth(MinAge = 18, MaxAge = 120)]
    public DateTime DateOfBirth { get; set; }
}
