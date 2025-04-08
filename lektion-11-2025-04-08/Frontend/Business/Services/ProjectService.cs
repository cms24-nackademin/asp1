using Business.Dtos;
using Business.Handlers;
using Business.Mappers;
using Business.Models;
using Data.Repositories;

namespace Business.Services;

public interface IProjectService
{
    Task<Project?> CreateProjectAsync(AddProjectDto dto);
    Task<bool> DeleteProjectAsync(string id);
    Task<Project?> GetProjectByIdAsync(string id);
    Task<IEnumerable<Project>?> GetProjectsAsync();
    Task<Project?> UpdateProjectAsync(UpdateProjectDto dto);
}

public class ProjectService(IProjectRepository projectRepository, ICacheHandler<IEnumerable<Project>> cacheHandler, IImageHandler imageHandler) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ICacheHandler<IEnumerable<Project>> _cacheHandler = cacheHandler;
    private const string _cacheKey = "Projects";
    private readonly IImageHandler _imageHandler = imageHandler;

    public async Task<Project?> CreateProjectAsync(AddProjectDto dto)
    {
        var entity = ProjectMapper.ToEntity(dto);
        entity.StatusId = 1;

        var imageFileName = await _imageHandler.SaveProjectImageAsync(dto.ImageFile!);
        entity.ImageFileName = imageFileName;

        await _projectRepository.AddAsync(entity);


        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.ProjectName == dto.ProjectName);
    }

    public async Task<IEnumerable<Project>?> GetProjectsAsync()
    {
        var models = _cacheHandler.GetFromCache(_cacheKey) ?? await UpdateCacheAsync();
        return models;
    }

    public async Task<Project?> GetProjectByIdAsync(string id)
    {
        var cached = _cacheHandler.GetFromCache(_cacheKey);

        var match = cached?.FirstOrDefault(x => x.Id == id);
        if (match != null)
            return match;

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Id == id);
    }

    public async Task<Project?> UpdateProjectAsync(UpdateProjectDto dto)
    {
        var entity = await _projectRepository.GetAsync(x => x.Id == dto.Id);
        if (entity == null)
            return null;

        entity = ProjectMapper.ToEntity(dto) ?? entity;
        await _projectRepository.UpdateAsync(entity);

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Id == dto.Id);
    }

    public async Task<bool> DeleteProjectAsync(string id)
    {
        var entity = await _projectRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return false;

        await _projectRepository.DeleteAsync(x => x.Id == id);
        await UpdateCacheAsync();
        return true;
    }

    public async Task<IEnumerable<Project>> UpdateCacheAsync()
    {
        var entities = await _projectRepository.GetAllAsync();
        var models = entities.Select(ProjectMapper.ToModel).ToList();

        _cacheHandler.SetCache(_cacheKey, models);
        return models;
    }

}