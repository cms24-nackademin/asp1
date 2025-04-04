namespace Domain.Responses;

public class ProjectResult : ResponseResult
{

}

public class ProjectResult<T> : ProjectResult
{
    public T? Result { get; set; }
}
