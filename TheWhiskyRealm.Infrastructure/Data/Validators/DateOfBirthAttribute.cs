using System.ComponentModel.DataAnnotations;

namespace TheWhiskyRealm.Infrastructure.Data.Validators;

public class DateOfBirthAttribute : ValidationAttribute
{
    public int MinAge { get; set; }
    public int MaxAge { get; set; }

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
