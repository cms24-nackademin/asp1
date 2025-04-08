namespace Business.Models;

public class Project
{
    public string Id { get; set; } = null!;
    public string? ImageFileName { get; set; }
    public string ProjectName { get; set; } = null!;
    public Client Client { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? Budget { get; set; }
    public User User { get; set; } = null!;
    public Status Status { get; set; } = null!;
    public DateTime Created { get; set; }
}
