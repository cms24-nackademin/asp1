namespace Domain.Responses;

public class ClientResult : ResponseResult
{

}

public class ClientResult<T> : ClientResult
{
    public T? Result { get; set; }
}
