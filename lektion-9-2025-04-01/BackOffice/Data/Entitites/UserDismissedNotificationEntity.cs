using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entitites;

public class UserDismissedNotificationEntity
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;
    public virtual UserEntity User { get; set; } = null!;


    [ForeignKey(nameof(Notification))]
    public string NotificationId { get; set; } = null!;
    public virtual NotificationEntity Notification { get; set; } = null!;
}