namespace TheWhiskyRealm.Core.Models.Whisky;

public class MyCollectionWhiskyModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string WhiskyType { get; set; } = string.Empty;
    public string ABV { get; set; } = string.Empty;
    public int? Age { get; set; }
    public string DistilleryName { get; set; } = string.Empty;
    public string Description {  get; set; } = string.Empty;

}
