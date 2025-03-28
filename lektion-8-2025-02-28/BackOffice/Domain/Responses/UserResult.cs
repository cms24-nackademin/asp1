namespace Domain.Responses;

public class UserResult : ResponseResult
{
    public string? SuccessMessage { get; set; }
}

public class UserResult<T> : UserResult
{
    public T? Result { get; set; }
}