using System.ComponentModel.DataAnnotations;

namespace TheWhiskyRealm.Infrastructure.Data.Enums;

public enum ArticleType
{
    [Display(Name = "General")]
    General = 1,

    [Display(Name = "Event")]
    Event = 2 ,

    [Display(Name = "Proffesional Review")]
    ProffesionalReview = 3
}
public static class ArticleTypeExtensions
{
    public static string GetDisplayName(this ArticleType articleType)
    {
        var memberInfo = typeof(ArticleType).GetMember(articleType.ToString());
        var displayAttribute = memberInfo.FirstOrDefault()?.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;

        return displayAttribute?.Name ?? articleType.ToString();
    }
}