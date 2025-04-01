namespace Domain.Responses;

public class NotificationResult : ResponseResult
{

}

public class NotificationResult<T> : NotificationResult
{
    public T? Result { get; set; }
}