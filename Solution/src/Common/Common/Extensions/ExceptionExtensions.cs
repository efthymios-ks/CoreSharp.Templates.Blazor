using CoreSharp.Extensions;
using System;
using System.Diagnostics;

namespace CoreSharp.Templates.Blazor.Common.Extensions;

/// <summary>
/// <see cref="Exception"/> extensions.
/// </summary>
public static class ExceptionExtensions
{
    /// <summary>
    /// Format <see cref="Exception.StackTrace"/>.
    /// </summary>
    public static Exception Format(this Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);

        return exception.GetInnermostException().Demystify();
    }
}
