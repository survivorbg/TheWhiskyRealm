using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static TheWhiskyRealm.Core.Constants.RoleConstants;

namespace TheWhiskyRealm.Areas.Admin.Controllers;

[Area(AdminArea)]
[Authorize(Roles = Administrator)]
public class AdminBaseController : Controller
{

}
