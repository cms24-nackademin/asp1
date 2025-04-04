namespace Domain.Models;

public class Notification
{
    public string Id { get; set; } = null!;
    public DateTime CreateDate { get; set; }
    public string Message { get; set; } = null!;
    public string Image { get; set; } = null!;
}
