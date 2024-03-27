namespace TheWhiskyRealm.Core.Models.Whisky;

public class DetailsWhiskyViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? Age { get; set; }

    public string AlcoholPercentage { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string DistilleryName { get; set; } = string.Empty;
    public string WhiskyType { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public string RegionName { get; set; } = string.Empty;
    public string AverageRating { get; set; } = string.Empty;
    //TEST
    public int Nose { get; set; }
    public int Taste { get; set; }
    public int Finish { get; set; }
    //TEST
    public ICollection<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();
}

