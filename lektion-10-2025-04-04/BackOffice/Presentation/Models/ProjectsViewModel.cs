using Domain.Models;

namespace Presentation.Models;

public class ProjectsViewModel
{
    public IEnumerable<Project> Projects { get; set; } = [];
    public AddProjectViewModel AddProjectViewModel { get; set; } = new AddProjectViewModel();
    public EditProjectViewModel EditProjectViewModel { get; set; } = new EditProjectViewModel();
}
