namespace TheWhiskyRealm.Core.Models.AdminArea.User;

public class UserInfoViewModel
{
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public int TotalReviews { get; set; }
    public int TotalRatings { get; set; }
    public int TotalComments { get; set; }
    public int JoinedEvents { get; set; }
    public int FavouriteWhiskies { get; set; }
    public int AddedWhiskies { get; set; }
    public int OrganisedEvents { get; set; }
    public int WrittenArticles { get; set; }
}
