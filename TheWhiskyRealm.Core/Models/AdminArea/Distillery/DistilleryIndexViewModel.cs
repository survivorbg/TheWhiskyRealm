namespace TheWhiskyRealm.Core.Models.AdminArea.Distillery;

public class DistilleryIndexViewModel
{
    public IEnumerable<DistilleryViewModel> Distilleries { get; set; } = new List<DistilleryViewModel>();
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; } = 20;
}
