using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SalesCRMApp.Controllers;

[Authorize(Roles= "Admin,Sales")]
public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        IQueryable<IdentityRole> roles = _roleManager.Roles;
        return View(roles);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(IdentityRole role)
    {
        if (!_roleManager.RoleExistsAsync(role.Name).GetAwaiter().GetResult())
        {
            await _roleManager.CreateAsync(role);
        }
        return RedirectToAction(nameof(Index));
    }
}
