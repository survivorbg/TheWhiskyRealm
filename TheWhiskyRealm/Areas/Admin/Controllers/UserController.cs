using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.User;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Areas.Admin.Controllers;

public class UserController : AdminBaseController
{
    private readonly IUserService userService;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly UserManager<ApplicationUser> userManager;

    public UserController(IUserService userService,
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager)
    {
        this.userService = userService;
        this.roleManager = roleManager;
        this.userManager = userManager;
    }

    [HttpGet]

    public async Task<IActionResult> Index(int currentPage = 1, int pageSize = 10)
    {
        var totalUsers = await userService.GetTotalUsersAsync();
        var users = await userService.GetAllUsersAsync(currentPage, pageSize);

        var model = new UserIndexViewModel
        {
            Users = users,
            CurrentPage = currentPage,
            TotalPages = (int)Math.Ceiling(totalUsers / (double)pageSize)
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var model = new UserFormModel();
        model.Roles = roleManager.Roles.Select(r => r.Name).ToList();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserFormModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth
            };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {

                var roleResult = await userManager.AddToRoleAsync(user, model.Role);

                if (roleResult.Succeeded)
                {

                    return RedirectToAction("Index");
                }
                else
                {

                    foreach (var error in roleResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            else
            {

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }


        model.Roles = roleManager.Roles.Select(r => r.Name).ToList();
        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await userManager.FindByIdAsync(id);
        
        if (user == null)
        {
            return NotFound();
        }
        var model = new UserFormModel()
        {
            Id = id,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
        };
        return View(model);
    }

    
    [HttpPost]
    public async Task<IActionResult> Delete(UserFormModel model)
    {
        var user = await userManager.FindByIdAsync(model.Id);
        if (user != null)
        {
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(user);
    }



}
