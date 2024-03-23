namespace TheWhiskyRealm.Infrastructure.Constants;

/// <summary>
/// Data Constants for the entity Whisky.
/// </summary>
public static class WhiskyDataConstants
{
    /// <summary>
    /// Minimum whisky name length.
    /// </summary>
    public const int WhiskyMinNameLength = 4;

    /// <summary>
    /// Maximum whisky name length.
    /// </summary>
    public const int WhiskyMaxNameLength = 70;

    /// <summary>
    /// Minimum whisky description length.
    /// </summary>
    public const int WhiskyMinDescLength = 50;

    /// <summary>
    /// Maximum whisky description length.
    /// </summary>
    public const int WhiskyMaxDescLength = 1500;

    /// <summary>
    /// Minimum value for the whisky's age.
    /// </summary>
    public const double WhiskyMinAge = 2;

    /// <summary>
    /// Maximum value for the whisky's age.
    /// </summary>
    public const double WhiskyMaxAge = 99;

    /// <summary>
    /// Minimum value for whisky's alcohol content percentage.
    /// </summary>
    public const double WhiskyMinABV = 40;

    /// <summary>
    /// Maximum value for whisky's alcohol content percentage.
    /// </summary>
    public const double WhiskyMaxABV = 94.8;
}
