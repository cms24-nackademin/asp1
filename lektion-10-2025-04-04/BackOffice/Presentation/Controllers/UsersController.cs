using Business.Services;
using Data.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers;

[Authorize]
public class UsersController(IUserService userService, AppDbContext context) : Controller
{
    private readonly IUserService _userService = userService;
    private readonly AppDbContext _context = context;

    [Route("admin/members")]
    public IActionResult Index()
    {
        return View();
    }


    [HttpGet]
    public async Task<JsonResult> SearchUsers(string term)
    {
        if (string.IsNullOrWhiteSpace(term))
            return Json(new List<object>());

        var users = await _context.Users
            .Where(x => x.FirstName!.Contains(term) || x.LastName!.Contains(term) || x.Email!.Contains(term))
            .Select(x => new { x.Id, x.Image, FullName = $"{x.FirstName} {x.LastName}" })
            .ToListAsync();

        return Json(users);
    }
}
