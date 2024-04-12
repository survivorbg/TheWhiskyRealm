using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWhiskyRealm.Core.Models.AdminArea.Region;

namespace TheWhiskyRealm.Core.Models.AdminArea.City;

public class CityIndexViewModel
{
    public IEnumerable<CityViewModel> Cities { get; set; } = new List<CityViewModel>();
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; } = 10;
}
