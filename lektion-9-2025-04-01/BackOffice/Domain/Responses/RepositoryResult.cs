namespace Domain.Responses;


public class RepositoryResult : ResponseResult
{

}

public class RepositoryResult<T> : RepositoryResult
{
    public T? Result { get; set; }
}
