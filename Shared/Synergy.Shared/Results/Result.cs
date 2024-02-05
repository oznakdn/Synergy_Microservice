namespace Synergy.Shared.Results;

public class Result : IResult
{
    public bool IsSuccess { get; protected set; }
    public string? Message { get; protected set; }
    public int? StatusCode { get; protected set; }
    public IEnumerable<string>? Errors { get; protected set; }

    public static Result Success(int? statusCode = null, string? message = null)
    {
        return new Result
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Message = message
        };
    }


    public static Result Failure(int? statusCode = null, string? error = null, IEnumerable<string>? errors = null)
    {
        return new Result
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = error,
            Errors = errors,
        };
    }

    public static Result Error(Exception exception, int statusCode)
    {
        return new Result
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = exception.Message,
            Errors = Enumerable.Empty<string>(),
        };
    }

}

public class Result<T> : IResult<T>
{
    public T? Value { get; private set; }
    public IEnumerable<T>? Values { get; private set; }

    public bool IsSuccess { get; private set; }

    public string? Message { get; private set; }

    public int? StatusCode { get; private set; }

    public IEnumerable<string>? Errors { get; private set; }


    public static Result<T> Failure(int? statusCode = null, string? error = null, IEnumerable<string>? errors = null)
    {
        return new Result<T>
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = error,
            Errors = errors,
        };
    }

    public static Result<T> Success(T value, int? statusCode = null, string? message = null)
    {
        return new Result<T>
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Message = message,
            Value = value
        };
    }

    public static Result<T> Success(IEnumerable<T> values, int? statusCode = null, string? message = null)
    {
        return new Result<T>
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Values = values,
            Message = message,
        };
    }

    public static Result<T> Success(int? statusCode = null, string? message = null)
    {
        return new Result<T>
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Message = message
        };
    }

    public static Result<T> Error(Exception exception, int? statusCode = null)
    {
        return new Result<T>
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = exception.Message,
        };
    }

}
