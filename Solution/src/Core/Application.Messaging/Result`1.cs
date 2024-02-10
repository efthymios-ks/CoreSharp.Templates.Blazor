using Application.Messaging.Interfaces;

namespace Application.Messaging;

public sealed class Result<TValue> : IResult<TValue>
{
    // Constructors 
    public Result(TValue value, string error)
    {
        IsOk = string.IsNullOrWhiteSpace(error);
        Value = value;
        Error = error;
    }

    // Properties 
    public bool IsOk { get; }
    public TValue Value { get; init; }
    public string Error { get; init; }
}
