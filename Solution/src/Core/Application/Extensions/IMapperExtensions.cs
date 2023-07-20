using AutoMapper;
using CoreSharp.Models.Pages;
using System;
using System.Collections.Generic;

namespace CoreSharp.Templates.Blazor.Application.Extensions;

/// <summary>
/// <see cref="IMapper"/> extensions.
/// </summary>
internal static class IMapperExtensions
{
    public static Page<TOut> MapPage<TIn, TOut>(this IMapper mapper, Page<TIn> page)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        ArgumentNullException.ThrowIfNull(page);

        return new(page.PageNumber,
                   page.PageSize,
                   page.TotalItems,
                   page.TotalPages,
                   mapper.Map<IEnumerable<TOut>>(page.Items));
    }
}
