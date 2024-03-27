using TheWhiskyRealm.Core.Models.Rating;
using TheWhiskyRealm.Core.Models.Review;

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
    public int Nose { get; set; }
    public int Taste { get; set; }
    public int Finish { get; set; }
    public ReviewFormModel Review { get; set; } = null!;
    public ICollection<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();
    
}

