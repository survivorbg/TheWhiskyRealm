using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheWhiskyRealm.Controllers;

[Authorize]
public class BaseController : Controller
{
    
}
