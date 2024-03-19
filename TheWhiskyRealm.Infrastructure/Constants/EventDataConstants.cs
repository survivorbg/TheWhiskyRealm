/// <summary>
/// Data Constants for the entity Event.
/// </summary>
namespace TheWhiskyRealm.Infrastructure.Constants;

public static class EventDataConstants
{
    /// Minimum length for the event title.
    /// </summary>
    public const int MinTitleLength = 10;

    /// <summary>
    /// Maximum length for the event title.
    /// </summary>
    public const int MaxTitleLength = 150;

    /// Minimum length for the event description.
    /// </summary>
    public const int MinDescLength = 50;

    /// <summary>
    /// Maximum length for the event description.
    /// </summary>
    public const int MaxDescLength = 1500;

    /// <summary>
    /// Minimum value for the event duration.
    /// </summary>
    public const double MinDuration = 1;

    /// <summary>
    /// Maximum value for the event duration.
    /// </summary>
    public const double MaxDuration = 72;

    /// <summary>
    /// Minimum value for the event price.
    /// </summary>
    public const double MinPrice = 10.00;

    /// <summary>
    /// Maximum value for the event price.
    /// </summary>
    public const double MaxPrice = 1000.00;
}

