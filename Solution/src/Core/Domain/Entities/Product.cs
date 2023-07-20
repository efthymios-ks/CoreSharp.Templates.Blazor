using CoreSharp.Templates.Blazor.Domain.Entities.Abstracts;
using System;

namespace CoreSharp.Templates.Blazor.Domain.Entities;

public class Product : AppEntityBase<Guid>
{
    // Properties
    public string Name { get; set; }
    public string Description { get; set; }
}
