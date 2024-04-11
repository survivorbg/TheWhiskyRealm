namespace TheWhiskyRealm.Core.Models.AdminArea.Distillery;

public class DistilleryViewModel
{
    public int Id { get; set; }
    public int YearFounded { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

}