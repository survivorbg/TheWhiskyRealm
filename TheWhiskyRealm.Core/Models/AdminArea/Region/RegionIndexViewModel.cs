namespace TheWhiskyRealm.Core.Models.AdminArea.Region;

public class RegionIndexViewModel
{
    public IEnumerable<RegionViewModel> Regions { get; set; } = new List<RegionViewModel>();
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; } = 15;
}
