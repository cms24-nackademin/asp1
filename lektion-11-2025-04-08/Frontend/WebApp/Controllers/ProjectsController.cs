using Business.Dtos;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers;

public class ProjectsController(IProjectService projectService, IClientService clientService, IStatusService statusService, IUserService userService) : Controller
{
    private readonly IProjectService _projectService = projectService;
    private readonly IClientService _clientService = clientService;
    private readonly IStatusService _statusService = statusService;
    private readonly IUserService _userService = userService;

    [Route("admin/projects")]
    public async Task<IActionResult> Index()
    {
        var viewModel = new ProjectsViewModel()
        {
            Projects = await SetProjectsAsync(),
            AddProjectFormData = new AddProjectViewModel
            {
                Clients = await SetClientSelectListItemsAsync(),
                Members = await SetUserSelectListItemsAsync(),
            },
            EditProjectFormData = new EditProjectViewModel
            {
                Clients = await SetClientSelectListItemsAsync(),
                Members = await SetUserSelectListItemsAsync(),
                Statuses = await SetStatusSelectListItemsAsync()
            }
        };

        return View(viewModel);
    }



    [HttpPost]
    public async Task<IActionResult> Create([FromForm] AddProjectDto dto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value!.Errors.Any())
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            return Json(new { success = false, errors });
        }

        var result = await _projectService.CreateProjectAsync(dto);
        if (result != null)
            return Json(new { success = true });

        return Json(new { success = false });
    }




    [HttpPost]
    public async Task<IActionResult> Update([FromForm] UpdateProjectDto dto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value!.Errors.Any())
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            return Json(new { success = false, errors });
        }

        var result = await _projectService.UpdateProjectAsync(dto);
        if (result != null)
            return Json(new { success = true });

        return Json(new { success = false });
    }





















    private async Task<IEnumerable<Project>> SetProjectsAsync()
    {
        var projects = await _projectService.GetProjectsAsync() ?? [];
        projects = projects.OrderByDescending(x => x.Created);
        return projects;
    }

    private async Task<IEnumerable<SelectListItem>> SetClientSelectListItemsAsync()
    {
        var clients = await _clientService.GetClientsAsync() ?? [];
        clients = clients.OrderBy(x => x.ClientName);

        var selectListItems = clients.Select(client => new SelectListItem
        {
            Value = client.Id,
            Text = client.ClientName
        });

        return selectListItems;
    }

    private async Task<IEnumerable<SelectListItem>> SetUserSelectListItemsAsync()
    {
        var users = await _userService.GetUsersAsync() ?? [];
        users = users.OrderBy(x => x.FirstName).ThenBy(x => x.LastName);

        var selectListItems = users.Select(user => new SelectListItem
        {
            Value = user.Id,
            Text = $"{user.FirstName} {user.LastName}"
        });

        return selectListItems;
    }

    private async Task<IEnumerable<SelectListItem>> SetStatusSelectListItemsAsync()
    {
        var statuses = await _statusService.GetStatusesAsync() ?? [];
        statuses = statuses.OrderBy(x => x.Id);

        var selectListItems = statuses.Select(status => new SelectListItem
        {
            Value = status.Id.ToString(),
            Text = status.StatusName
        });

        return selectListItems;
    }
}
