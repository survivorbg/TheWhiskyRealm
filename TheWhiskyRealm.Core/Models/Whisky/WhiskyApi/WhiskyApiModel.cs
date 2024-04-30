namespace TheWhiskyRealm.Core.Models.Whisky.WhiskyApi;

public class WhiskyApiModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? Age { get; set; }
    public double AlcoholPercentage { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Distillery { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string WhiskyType { get; set; } = string.Empty;
}
