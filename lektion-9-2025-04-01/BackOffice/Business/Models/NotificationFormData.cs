using Data.Entitites;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class NotificationFormData
{
    public int NotificationTypeId { get; set; }
    public int NotificationTargetId { get; set; }
    public string Message { get; set; } = null!;
    public string? Image { get; set; }
    public string? UserId { get; set; }
}