using System.ComponentModel.DataAnnotations;

namespace TheWhiskyRealm.Infrastructure.Data.Enums;

public enum MedalType
{
    [Display(Name = "Gold")]
    Gold = 1,

    [Display(Name = "Silver")]
    Silver = 2,

    [Display(Name = "Bronze")]
    Bronze = 3
}

public static class MedalTypeExtensions
{
    public static string GetDisplayName(this MedalType medalType)
    {
        var memberInfo = typeof(MedalType).GetMember(medalType.ToString());
        var displayAttribute = memberInfo.FirstOrDefault()?.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;

        return displayAttribute?.Name ?? medalType.ToString();
    }
}