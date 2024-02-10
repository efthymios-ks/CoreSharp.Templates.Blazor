using System.Diagnostics.CodeAnalysis;

namespace Application.Messaging.Interfaces;

[SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "<Pending>")]
public interface IResult<out TValue> : IResult
{
    // Properties 
    bool IsOk { get; }
    TValue Value { get; }
    string Error { get; }
}
