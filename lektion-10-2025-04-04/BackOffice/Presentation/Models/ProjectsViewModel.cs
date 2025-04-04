using Domain.Models;

namespace Presentation.Models;

public class ProjectsViewModel
{
    public IEnumerable<Project> Projects { get; set; } = [];
}
