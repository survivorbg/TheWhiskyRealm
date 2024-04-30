namespace TheWhiskyRealm.Core.Models.Whisky.WhiskyApi;

public class DistilleryApiModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int YearFounded { get; set; }
    public string Region { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}
