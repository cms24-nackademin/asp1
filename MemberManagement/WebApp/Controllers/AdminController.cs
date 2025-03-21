using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.Controllers;

[Authorize]
public class AdminController(IMemberService memberService) : Controller
{
    private readonly IMemberService _memberService = memberService;

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Projects()
    {
        return View();
    }

    public async Task<IActionResult> Members()
    {
        var members = await _memberService.GetMembersAsync();
        return View(members);
    }

    public IActionResult Clients()
    {
        return View();
    }
}
