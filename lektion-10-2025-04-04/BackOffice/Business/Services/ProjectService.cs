using Business.Models;
using Data.Repositories;
using Domain.Models;
using Domain.Responses;

namespace Business.Services;

public interface IProjectService
{
    Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync();
    Task<ProjectResult<Project>> GetProjectAsync(string id);
    Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData);
    Task<ProjectResult> UpdateProjectAsync(EditProjectFormData formData);
    Task<ProjectResult> DeleteProjectAsync(string id);
}


public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData)
    {
        throw new NotImplementedException();
    }

    public async Task<ProjectResult> DeleteProjectAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<ProjectResult<Project>> GetProjectAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ProjectResult> UpdateProjectAsync(EditProjectFormData formData)
    {
        throw new NotImplementedException();
    }
}
