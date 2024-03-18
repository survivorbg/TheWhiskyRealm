namespace TheWhiskyRealm.Infrastructure.Constants;

/// <summary>
/// Data Constants for the entity Whisky.
/// </summary>
public static class WhiskyDataConstants
{
    /// <summary>
    /// Minimum whisky name length.
    /// </summary>
    public const int MinNameLength = 4;

    /// <summary>
    /// Maximum whisky name length.
    /// </summary>
    public const int MaxNameLength = 70;

    /// <summary>
    /// Minimum whisky description length.
    /// </summary>
    public const int MinDescLength = 50;

    /// <summary>
    /// Maximum whisky description length.
    /// </summary>
    public const int MaxDescLength = 1500;

    /// <summary>
    /// Minimum value for the whisky's age.
    /// </summary>
    public const double MinAge = 2;

    /// <summary>
    /// Maximum value for the whisky's age.
    /// </summary>
    public const double MaxAge = 99;

    /// <summary>
    /// Minimum value for whisky's alcohol content percentage.
    /// </summary>
    public const double MinABV = 40;

    /// <summary>
    /// Maximum value for whisky's alcohol content percentage.
    /// </summary>
    public const double MaxABV = 94.8;
}
