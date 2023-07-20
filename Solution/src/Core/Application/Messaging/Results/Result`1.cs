using CoreSharp.Templates.Blazor.Application.Messaging.Results.Interfaces;
using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;

namespace CoreSharp.Templates.Blazor.Application.Messaging.Results;
 
public sealed class Result<TValue> : IResult<TValue>
{
    // Constructors 
    public Result(TValue value, string error)
    {
        IsOk = string.IsNullOrWhiteSpace(Error);
        Value = value;
        Error = error;
    }

    // Properties 
    public bool IsOk { get; }
    public TValue Value { get; }
    public string Error { get; }

    // Methods 
    public static implicit operator Result<TValue>(TValue value)
        => new(value, default);

    public static implicit operator Result<TValue>(string error)
        => new(default, error);
}
