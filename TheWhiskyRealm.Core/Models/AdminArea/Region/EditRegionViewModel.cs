using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWhiskyRealm.Core.Models.AdminArea.Region;

public class EditRegionViewModel : AddRegionViewModel //TODO - merge it in one view model
{
    public new int CountryId { get; set; }
}
