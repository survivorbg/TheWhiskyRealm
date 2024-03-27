namespace TheWhiskyRealm.Core.Models.Whisky;

public class AwardViewModel
{
    //TODO Add validations
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string AwardsCeremony { get; set; } = string.Empty;
    public string MedalType { get; set; } = string.Empty;
    public int Year { get; set; }

}