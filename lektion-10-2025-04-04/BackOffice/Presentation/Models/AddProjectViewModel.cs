using Data.Entitites;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class AddProjectViewModel
{
    public string? Image { get; set; }
    public string ProjectName { get; set; } = null!;


    [Display(Name = "Description", Prompt = "Type something")]
    public string? Description { get; set; }
    
    
    
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? Budget { get; set; }
    public string ClientId { get; set; } = null!;

}
