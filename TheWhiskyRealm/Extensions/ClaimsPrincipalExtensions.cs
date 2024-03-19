using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Retrieves the unique identifier of the user.
    /// </summary>
    /// <param name="user">The ClaimsPrincipal instance representing the user.</param>
    /// <returns>The unique identifier of the user as a string, or null if not found.</returns>
    public static string Id(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    /// <summary>
    /// Checks if the user associated with the provided ClaimsPrincipal instance has the Administrator role.
    /// </summary>
    /// <param name="user">The ClaimsPrincipal instance representing the user.</param>
    /// <returns>True if the user has the Administrator role, otherwise false.</returns>
    public static bool IsAdmin(this ClaimsPrincipal user)
    {
        return user.IsInRole("Administrator");
    }

}
