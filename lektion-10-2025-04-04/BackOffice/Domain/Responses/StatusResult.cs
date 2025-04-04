namespace Domain.Responses;

public class StatusResult : ResponseResult
{

}

public class StatusResult<T> : ClientResult
{
    public T? Result { get; set; }
}