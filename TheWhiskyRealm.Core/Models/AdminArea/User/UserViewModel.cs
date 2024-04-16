using System.ComponentModel.DataAnnotations;

namespace TheWhiskyRealm.Core.Models.AdminArea.User;

public class UserViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    [Display(Name = "Date of Birth")]
    public DateTime DateOfBirth { get; set; }
}
