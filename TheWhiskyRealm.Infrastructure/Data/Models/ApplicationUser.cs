using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    public int Age { get; set; }
}
