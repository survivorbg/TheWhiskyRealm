using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TheWhiskyRealm.Infrastructure.Data.Validators;

namespace TheWhiskyRealm.Core.Models.AdminArea.User;

public class UserEditViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    [Required]
    [PersonalData]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DateOfBirth(MinAge = 18, MaxAge = 120)]
    public DateTime DateOfBirth { get; set; }
    [Required]
    public string Role { get; set; } = string.Empty;

    public List<string> Roles { get; set; } = new List<string>();
}
