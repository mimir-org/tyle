using System;
using System.Collections.Generic;
using System.Linq;
using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models;

public class TerminalLibDm : IVersionable<TerminalLibAm>, IVersionObject, ILogable
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Iri { get; set; }
    public string TypeReference { get; set; }
    public string Version { get; set; }
    public string FirstVersionId { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public int CompanyId { get; set; }
    public State State { get; set; }
    public string Color { get; set; }
    public string Description { get; set; }
    public string ParentId { get; set; }
    public TerminalLibDm Parent { get; set; }
    public ICollection<TerminalLibDm> Children { get; set; }
    public ICollection<AspectObjectTerminalLibDm> TerminalAspectObjects { get; set; }
    public ICollection<AttributeLibDm> Attributes { get; set; }

    #region IVersionable

    public Validation HasIllegalChanges(TerminalLibAm other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        var validation = new Validation();

        if (Name != other.Name)
            validation.AddNotAllowToChange(nameof(Name));

        //Attributes
        var terminalAttributeAms = new List<TerminalAttributeLibAm>();
        var terminalAttributeDms = new List<TerminalAttributeLibDm>();

        terminalAttributeAms.AddRange(other.TerminalAttributes ?? new List<TerminalAttributeLibAm>());
        terminalAttributeDms.AddRange(TerminalAttributes ?? new List<TerminalAttributeLibDm>());

        if (terminalAttributeDms.Select(y => y.AttributeId).Any(id => terminalAttributeAms.Select(x => x.AttributeId).All(x => x != id)))
            validation.AddNotAllowToChange(nameof(TerminalAttributes), "It is not allowed to remove or change attributes");

        if (ParentId != other.ParentId)
            validation.AddNotAllowToChange(nameof(ParentId));

        validation.IsValid = !validation.Result.Any();
        return validation;

    }

    public VersionStatus CalculateVersionStatus(TerminalLibAm other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        var minor = false;
        var major = false;

        if (CompanyId != other.CompanyId)
            minor = true;

        if (TypeReference != other.TypeReference)
            minor = true;

        if (Description != other.Description)
            minor = true;

        if (Color != other.Color)
            minor = true;

        //Attributes
        var terminalAttributeAms = new List<TerminalAttributeLibAm>();
        var terminalAttributeDms = new List<TerminalAttributeLibDm>();

        terminalAttributeAms.AddRange(other.TerminalAttributes ?? new List<TerminalAttributeLibAm>());
        terminalAttributeDms.AddRange(TerminalAttributes ?? new List<TerminalAttributeLibDm>());

        if (!terminalAttributeDms.Select(x => x.AttributeId).SequenceEqual(terminalAttributeAms.Select(x => x.AttributeId)))
        {
            major = true;
        }

        return major ? VersionStatus.Major : minor ? VersionStatus.Minor : VersionStatus.NoChange;

    }

    #endregion IVersionable

    #region ILogable

    public LogLibAm CreateLog(LogType logType, string logTypeValue, string comment)
    {
        return new LogLibAm
        {
            ObjectId = Id,
            ObjectFirstVersionId = FirstVersionId,
            ObjectType = nameof(TerminalLibDm),
            ObjectName = Name,
            ObjectVersion = Version,
            LogType = logType,
            LogTypeValue = logTypeValue,
            Comment = comment
        };
    }

    #endregion ILogable
}