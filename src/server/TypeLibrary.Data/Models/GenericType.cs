using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;

namespace TypeLibrary.Data.Models;

public class GenericType
{
    public Guid Id { get; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Version { get; private set; }
    public DateTimeOffset CreatedOn { get; }
    public string CreatedBy { get; }
    public ICollection<string> ContributedBy { get; }
    public DateTimeOffset LastUpdateOn { get; private set; }

    protected GenericType(string name, string? description, string createdBy)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Version = "1.0";
        CreatedOn = DateTimeOffset.Now;
        CreatedBy = createdBy;
        ContributedBy = new HashSet<string>();
        LastUpdateOn = CreatedOn;
    }
}
