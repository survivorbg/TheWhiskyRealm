using System.ComponentModel.DataAnnotations;

namespace TheWhiskyRealm.Infrastructure.Data.Validators;

/// <summary>
/// The DateOfBirthAttribute class is a validation attribute that checks if the user's date of birth is valid.
/// </summary>
public class DateOfBirthAttribute : ValidationAttribute
{
    /// <summary>
    /// Gets or sets the minimum age.
    /// </summary>
    public int MinAge { get; set; }

    /// <summary>
    /// Gets or sets the maximum age.
    /// </summary>
    public int MaxAge { get; set; }


    /// <summary>
    /// Validates the date of birth.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="validationContext">The validation context.</param>
    /// <returns>A ValidationResult that indicates whether the date of birth is valid.</returns>
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime date;
        if (value == null || !(value is DateTime) ||
            !DateTime.TryParse(value.ToString(), out date))
        {
            return new ValidationResult("Invalid Date of Birth");
        }

        if (date.AddYears(MinAge) > DateTime.Now)
        {
            return new ValidationResult($"You must be at least {MinAge} years old");
        }

        if (date.AddYears(MaxAge) < DateTime.Now)
        {
            return new ValidationResult($"You cannot be more than {MaxAge} years old");
        }

        return ValidationResult.Success;
    }
}
