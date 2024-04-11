using TheWhiskyRealm.Core.Models.AdminArea.Whisky;

namespace TheWhiskyRealm.Core.Models.AdminArea.Distillery;

public class DistilleryInfoModel : DistilleryViewModel
{
    public IEnumerable<WhiskyDistilleryViewModel> Whiskies { get; set; } = new List<WhiskyDistilleryViewModel>();
}
