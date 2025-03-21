using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.ViewModels;

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
        var viewModel = new MembersViewModel
        {
            Members = await _memberService.GetMembersAsync()
        };

        return View(viewModel);
    }

    public IActionResult Clients()
    {
        return View();
    }
}
