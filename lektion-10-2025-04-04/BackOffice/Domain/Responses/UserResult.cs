namespace Domain.Responses;

public class UserResult : ResponseResult
{

}

public class UserResult<T> : UserResult
{
    public T? Result { get; set; }
}