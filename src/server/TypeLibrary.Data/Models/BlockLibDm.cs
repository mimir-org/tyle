using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models;

/// <summary>
/// Block domain model
/// </summary>
public class BlockLibDm : IVersionable<BlockLibAm>, IVersionObject, ILogable, IEquatable<BlockLibDm>
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
    public Aspect Aspect { get; set; }
    public string PurposeName { get; set; }
    public string RdsId { get; set; }
    public RdsLibDm Rds { get; set; }
    public string Symbol { get; set; }
    public string Description { get; set; }
    public virtual ICollection<BlockTerminalLibDm> BlockTerminals { get; set; }
    public virtual ICollection<BlockAttributeLibDm> BlockAttributes { get; set; }
    public virtual List<SelectedAttributePredefinedLibDm> SelectedAttributePredefined { get; set; }

    #region IVersionable

    public Validation HasIllegalChanges(BlockLibAm other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        var validation = new Validation();

        if (Aspect != other.Aspect)
            validation.AddNotAllowToChange(nameof(Aspect));

        if (CompanyId != other.CompanyId)
            validation.AddNotAllowToChange(nameof(CompanyId));

        //Attributes
        var attributeAmIds = new List<string>();
        var blockAttributeDms = new List<BlockAttributeLibDm>();

        attributeAmIds.AddRange(other.Attributes ?? new List<string>());
        blockAttributeDms.AddRange(BlockAttributes ?? new List<BlockAttributeLibDm>());

        if (blockAttributeDms.Select(y => y.AttributeId).Any(id => attributeAmIds.All(x => x != id)))
            validation.AddNotAllowToChange(nameof(BlockAttributes), "It is not allowed to remove or change attributes");

        //Terminals
        BlockTerminals ??= new List<BlockTerminalLibDm>();
        other.BlockTerminals ??= new List<BlockTerminalLibAm>();
        var otherTerminals = other.BlockTerminals.Select(x => (x.TerminalId, x.ConnectorDirection));
        if (BlockTerminals.Select(y => (y.TerminalId, y.ConnectorDirection)).Any(identifier => otherTerminals.Select(x => x).All(x => x != identifier)))
        {
            validation.AddNotAllowToChange(nameof(BlockTerminals), "It is not allowed to remove terminals");
        }

        foreach (var terminal in other.BlockTerminals)
        {
            if (!BlockTerminals.Select(x => (x.TerminalId, x.ConnectorDirection)).Contains((terminal.TerminalId, terminal.ConnectorDirection))) continue;

            var current = BlockTerminals.FirstOrDefault(x => x.TerminalId == terminal.TerminalId && x.ConnectorDirection == terminal.ConnectorDirection);
            if (terminal.MaxQuantity != 0 && current?.MaxQuantity > terminal.MaxQuantity)
                validation.AddNotAllowToChange(nameof(BlockTerminals), "It is not allowed to lower max quantity of terminals");
        }

        //Predefined attributes
        SelectedAttributePredefined ??= new List<SelectedAttributePredefinedLibDm>();
        other.SelectedAttributePredefined ??= new List<SelectedAttributePredefinedLibAm>();
        if (SelectedAttributePredefined.Select(y => y.Key).Any(key => other.SelectedAttributePredefined.Select(x => x.Key).All(x => x != key)))
        {
            validation.AddNotAllowToChange(nameof(SelectedAttributePredefined), "It is not allowed to remove items from predefined attributes");
        }

        validation.IsValid = !validation.Result.Any();
        return validation;
    }

    public VersionStatus CalculateVersionStatus(BlockLibAm other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        var minor = false;
        var major = false;

        if (Name != other.Name)
            minor = true;

        if (TypeReference != other.TypeReference)
            minor = true;

        if (PurposeName != other.PurposeName)
            minor = true;

        if (RdsId != other.RdsId)
            minor = true;

        if (Symbol != other.Symbol)
            minor = true;

        if (Description != other.Description)
            minor = true;

        //Attributes
        var attributeAmIds = new List<string>();
        var blockAttributeDms = new List<BlockAttributeLibDm>();

        attributeAmIds.AddRange(other.Attributes ?? new List<string>());
        blockAttributeDms.AddRange(BlockAttributes ?? new List<BlockAttributeLibDm>());

        if (!blockAttributeDms.Select(x => x.AttributeId).Order().SequenceEqual(attributeAmIds.Order()))
        {
            major = true;
        }

        // Block Terminals
        BlockTerminals ??= new List<BlockTerminalLibDm>();
        other.BlockTerminals ??= new List<BlockTerminalLibAm>();
        var otherTerminals = other.BlockTerminals.Select(x => (x.TerminalId, x.ConnectorDirection, MaxQuantity: x.MaxQuantity == 0 ? int.MaxValue : x.MaxQuantity)).OrderBy(x => x.TerminalId).ThenBy(x => x.ConnectorDirection).ThenBy(x => x.MaxQuantity);
        if (!BlockTerminals.Select(x => (x.TerminalId, x.ConnectorDirection, x.MaxQuantity)).OrderBy(x => x.TerminalId).ThenBy(x => x.ConnectorDirection).ThenBy(x => x.MaxQuantity).SequenceEqual(otherTerminals))
            major = true;

        // Attribute Predefined
        SelectedAttributePredefined ??= new List<SelectedAttributePredefinedLibDm>();
        other.SelectedAttributePredefined ??= new List<SelectedAttributePredefinedLibAm>();
        if (!SelectedAttributePredefined.Select(x => x.Key).Order().SequenceEqual(other.SelectedAttributePredefined.Select(x => x.Key).Order()))
            major = true;

        return major ? VersionStatus.Major : minor ? VersionStatus.Minor : VersionStatus.NoChange;
    }

    #endregion IVersionable

    #region ILogable

    public LogLibDm CreateLog(LogType logType, string logTypeValue, string createdBy)
    {
        return new LogLibDm
        {
            ObjectId = Id,
            ObjectFirstVersionId = FirstVersionId,
            ObjectType = nameof(BlockLibDm),
            ObjectName = Name,
            ObjectVersion = Version,
            LogType = logType,
            LogTypeValue = logTypeValue,
            Created = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }

    #endregion ILogable

    public bool Equals(BlockLibDm other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        if (Name != other.Name || Aspect != other.Aspect || RdsId != other.RdsId || PurposeName != other.PurposeName || CompanyId != other.CompanyId || TypeReference != other.TypeReference || Description != other.Description || Symbol != other.Symbol)
            return false;

        // Block Attributes
        BlockAttributes ??= new List<BlockAttributeLibDm>();
        other.BlockAttributes ??= new List<BlockAttributeLibDm>();
        if (!BlockAttributes.Select(x => x.AttributeId).Order().SequenceEqual(other.BlockAttributes.Select(x => x.AttributeId).Order()))
        {
            return false;
        }

        // Block Terminals
        BlockTerminals ??= new List<BlockTerminalLibDm>();
        other.BlockTerminals ??= new List<BlockTerminalLibDm>();
        if (!BlockTerminals.Select(x => (x.TerminalId, x.ConnectorDirection)).Order()
                .SequenceEqual(other.BlockTerminals.Select(x => (x.TerminalId, x.ConnectorDirection)).Order()))
        {
            return false;
        }

        // Attribute Predefined
        SelectedAttributePredefined ??= new List<SelectedAttributePredefinedLibDm>();
        other.SelectedAttributePredefined ??= new List<SelectedAttributePredefinedLibDm>();
        if (!SelectedAttributePredefined.Select(x => x.Key).Order()
                .SequenceEqual(other.SelectedAttributePredefined.Select(x => x.Key).Order()))
        {
            return false;
        }

        return true;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((BlockLibDm) obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}