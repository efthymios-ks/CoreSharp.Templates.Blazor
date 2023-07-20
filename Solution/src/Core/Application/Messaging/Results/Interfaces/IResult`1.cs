namespace CoreSharp.Templates.Blazor.Application.Messaging.Results.Interfaces;

internal interface IResult<out TValue> : IResult
{
    // Properties 
    bool IsOk { get; }
    TValue Value { get; }
    string Error { get; }
}
