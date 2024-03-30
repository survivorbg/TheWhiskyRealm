namespace TheWhiskyRealm.Core.Models.Whisky;

public class MyCollectionPagination
{
    public ICollection<MyCollectionWhiskyModel> Whiskies { get; set; } = new List<MyCollectionWhiskyModel>();
    public int page { get; set; }
    public int pageSize { get; set; }
    public int allWhiskies { get; set; }
}
