using TheWhiskyRealm.Core.Models.AdminArea.Distillery;

namespace TheWhiskyRealm.Core.Models.AdminArea.Country;

public class RegionInfoViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<DistilleryRegionViewModel> Distilleries { get; set; } = new List<DistilleryRegionViewModel>();
}
