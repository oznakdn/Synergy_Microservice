namespace Synergy.Shared.Results;

public class Result : IResult
{
    public bool IsSuccess { get; protected set; }
    public string? Message { get; protected set; }
    public int StatusCode { get; protected set; }
    public IEnumerable<string>? Errors { get; protected set; }

    public static Result Success(int statusCode)
    {
        return new Result
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Message = string.Empty,
            Errors = Enumerable.Empty<string>(),
        };
    }

    public static Result Success(int statusCode, string message)
    {
        return new Result
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Message = message,
            Errors = Enumerable.Empty<string>(),
        };
    }

    public static Result Failure(int statusCode)
    {
        return new Result
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = string.Empty,
            Errors = Enumerable.Empty<string>(),
        };
    }

    public static Result Failure(int statusCode,string message)
    {
        return new Result
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = message,
            Errors = Enumerable.Empty<string>(),
        };
    }

    public static Result Failure(int statusCode, IEnumerable<string> errors)
    {
        return new Result
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = string.Empty,
            Errors = errors
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

public class Result<T> : Result,IResult<T>
{
    public T? Value { get; private set; }
    public IEnumerable<T>? Values { get; private set; }

    public static Result<T> Success(int statusCode,T value)
    {
        return new Result<T>
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Value =value,
            Message = string.Empty,
            Errors = Enumerable.Empty<string>(),
        };
    }

    public static Result<T> Success(int statusCode, T value, string message)
    {
        return new Result<T>
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Value = value,
            Message = message,
            Errors = Enumerable.Empty<string>(),
        };
    }

    public static Result<T> Success(int statusCode, IEnumerable<T> values)
    {
        return new Result<T>
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Value = default(T)!,
            Values = values,
            Message = string.Empty,
            Errors = Enumerable.Empty<string>(),
        };
    }

    public static Result<T> Success(int statusCode, IEnumerable<T> values, string message)
    {
        return new Result<T>
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Value = default(T)!,
            Values = values,
            Message = message,
            Errors = Enumerable.Empty<string>(),
        };
    }

}
