using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TheWhiskyRealm.Infrastructure.Data.Validators;

namespace TheWhiskyRealm.Core.Models.AdminArea.User;

public class UserFormModel
{
    public string Id { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [PersonalData]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DateOfBirth(MinAge = 18, MaxAge = 120)]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty;

    public List<string> Roles { get; set; } = new List<string>();
}
