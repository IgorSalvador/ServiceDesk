namespace ServiceDesk.Application.Common.Models;

public class Result
{
    public bool Succeeded { get; protected set; }
    public string[] Errors { get; protected set; }

    protected Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public static Result Success() =>
        new(true, []);

    public static Result Failure(IEnumerable<string> errors) =>
        new(false, errors);

    public static Result Failure(string error) =>
        new(false, [error]);
}

public class Result<T> : Result
{
    public T Data { get; private set; }

    private Result(bool succeeded, T data, IEnumerable<string> errors)
        : base(succeeded, errors)
    {
        Data = data;
    }

    public static Result<T> Success(T data) =>
        new(true, data, []);

    public new static Result<T> Failure(IEnumerable<string> errors) =>
        new(false, default!, errors);

    public new static Result<T> Failure(string error) =>
        new Result<T>(false, default!, [error]);
}