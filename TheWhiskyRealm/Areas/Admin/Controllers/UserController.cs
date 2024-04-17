using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.User;
using TheWhiskyRealm.Infrastructure.Data.Models;
using static TheWhiskyRealm.Core.Constants.RoleConstants;

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
        model.Roles = roleManager.Roles
            .Where(r=>r.Name != Administrator)
            .Select(r => r.Name).ToList();
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

                    return RedirectToAction(nameof(Index));
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


        model.Roles = roleManager.Roles
           .Where(r => r.Name != Administrator)
           .Select(r => r.Name)
           .ToList();

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
        
        if (await userManager.IsInRoleAsync(user, Administrator))
        {
            return BadRequest();
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
            if (await userManager.IsInRoleAsync(user, Administrator))
            {
                return BadRequest();
            }

            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> EditRole(string id)
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

        if (await userManager.IsInRoleAsync(user, Administrator))
        {
            return BadRequest();
        }

        var model = new UserEditViewModel
        {
            Id = user.Id,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            Roles = roleManager.Roles.Where(r => r.Name != Administrator).Select(r => r.Name)
           .ToList()
    };

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> EditRole(UserEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                if (await userManager.IsInRoleAsync(user, Administrator))
                {
                    return BadRequest();
                }

                var currentRole = await userManager.GetRolesAsync(user);
                var existingRole = currentRole.SingleOrDefault();
                if (existingRole == model.Role)
                {
                    return RedirectToAction(nameof(Index));
                }

                if (existingRole != null)
                {
                    var removeRoleResult = await userManager.RemoveFromRoleAsync(user, existingRole);
                    if (!removeRoleResult.Succeeded)
                    {
                        model.Roles = roleManager.Roles.Where(r => r.Name != Administrator).Select(r => r.Name).ToList(); 
                        return View(model);
                    }
                }

                var addRoleResult = await userManager.AddToRoleAsync(user, model.Role);
                if (!addRoleResult.Succeeded)
                {
                    model.Roles = roleManager.Roles.Where(r => r.Name != Administrator).Select(r => r.Name).ToList();
                    return View(model);
                }

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        model.Roles = roleManager.Roles.Where(r => r.Name != Administrator).Select(r => r.Name).ToList();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Lock(string id)
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

        if (await userManager.IsInRoleAsync(user, Administrator))
        {
            return BadRequest();
        }

        await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Unlock(string id)
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

        if (await userManager.IsInRoleAsync(user, Administrator))
        {
            return BadRequest();
        }

        await userManager.SetLockoutEndDateAsync(user, null);

        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Info(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var model = await userService.GetUserInfoAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

}
