namespace Domain.Responses;

public class AuthResult : ResponseResult
{
    public string? SuccessMessage { get; set; }
}

public class AuthResult<T> : AuthResult
{
    public T? Result { get; set; }
}