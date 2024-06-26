﻿namespace TheWhiskyRealm.Core.Models.Whisky;

public class AllWhiskyModel
{
    /// <summary>
    /// Represents the whisky unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Represents the whisky name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Represent the whisky age, if stated.
    /// </summary>
    public int? Age { get; set; }

    /// <summary>
    /// Represents the alcohol percentage(ABV) of this whisky.
    /// </summary>
    public double AlcoholPercentage { get; set; }

    /// <summary>
    /// Represents the whisky type.
    /// </summary>
    public string WhiskyType { get; set; } = string.Empty;
    /// <summary>
    /// Represents the number ot reviews associated with this whisky.
    /// </summary>
    public int Reviews { get; set; }

    public double AverageRating { get; set; }
    public string ImageURL { get; set; } = string.Empty;
}
