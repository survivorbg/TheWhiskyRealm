namespace TheWhiskyRealm.Core.Constants;

/// <summary>
/// The MessageConstants class provides constant string messages for validation and user interaction.
/// </summary>
public static class MessageConstants
{
    /// <summary>
    /// Message displayed when a required field is not provided.
    /// </summary>
    public const string RequiredMessage = "The {0} field is required!";

    /// <summary>
    /// Message displayed when the length of a field's value is not within the specified range.
    /// </summary>
    public const string LengthMessage = "The field {0} must be between {2} and {1} characters long!";

    /// <summary>
    /// Message displayed when the value of a field is not within the specified range.
    /// </summary>
    public const string ValueMessage = "The field {0} value must be between {1} and {2}!";

    /// <summary>
    /// Message displayed when the user needs to select whether they recommend a whisky or not.
    /// </summary>
    public const string RecommendBoolMessage = "Please select whether you recommend this whisky or not.";

    /// <summary>
    /// Message displayed when the user needs to select a valid option.
    /// </summary>
    public const string BoolValidOption = "Please select a valid option.";
}