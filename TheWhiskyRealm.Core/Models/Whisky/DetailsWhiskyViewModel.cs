﻿using TheWhiskyRealm.Core.Models.Award;
using TheWhiskyRealm.Core.Models.Review;
using TheWhiskyRealm.Infrastructure.Data;

namespace TheWhiskyRealm.Core.Models.Whisky;

public class DetailsWhiskyViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? Age { get; set; }

    public string AlcoholPercentage { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string ImageURL { get; set; } = string.Empty;
    public string DistilleryName { get; set; } = string.Empty;
    public int DistilleryId { get; set; }
    public string WhiskyType { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public string RegionName { get; set; } = string.Empty;
    public string AverageRating { get; set; } = string.Empty;
    public string? PublishedBy {  get; set; }
    public bool IsFavourite { get; set; }
    public int Nose { get; set; }
    public int Taste { get; set; }
    public int Finish { get; set; }
    public ReviewFormModel Review { get; set; } = null!;
    public ICollection<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();
    public ICollection<AwardViewModel> Awards { get; set; } = new List<AwardViewModel>();
    
}

