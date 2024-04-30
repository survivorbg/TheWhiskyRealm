namespace TheWhiskyRealm.Core.Models.Whisky.WhiskyApi;

public class DistilleryDetailsApiModel : DistilleryApiModel
{
    public IEnumerable<WhiskyApiModel> Whiskies { get; set; } = new List<WhiskyApiModel>();
}
