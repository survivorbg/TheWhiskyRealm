/// <summary>
/// Data Constants for the entity Event.
/// </summary>
namespace TheWhiskyRealm.Infrastructure.Constants;

public static class EventDataConstants
{
    /// Minimum length for the event title.
    /// </summary>
    public const int EventMinTitleLength = 10;

    /// <summary>
    /// Maximum length for the event title.
    /// </summary>
    public const int EventMaxTitleLength = 150;

    /// Minimum length for the event description.
    /// </summary>
    public const int EventMinDescLength = 50;

    /// <summary>
    /// Maximum length for the event description.
    /// </summary>
    public const int EventMaxDescLength = 1500;

    /// <summary>
    /// Minimum value for the event duration.
    /// </summary>
    public const double EventMinDuration = 1;

    /// <summary>
    /// Maximum value for the event duration.
    /// </summary>
    public const double EventMaxDuration = 72;

    /// <summary>
    /// Minimum value for the event price.
    /// </summary>
    public const double EventMinPrice = 10.00;

    /// <summary>
    /// Maximum value for the event price.
    /// </summary>
    public const double EventMaxPrice = 1000.00;
}

