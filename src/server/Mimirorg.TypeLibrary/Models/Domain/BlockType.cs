using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Domain;

/// <summary>
/// Block domain model
/// </summary>
public class BlockType // : IVersionable<BlockLibAm>, IVersionObject, IEquatable<BlockType>, ILogable
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string Version { get; set; } = "1.0";
    public DateTimeOffset CreatedOn { get; set; }
    public required string CreatedBy { get; set; }
    public ICollection<string> ContributedBy { get; set; } = new List<string>();
    public DateTimeOffset LastUpdateOn { get; set; }
    //public int CompanyId { get; set; }
    //public State State { get; set; }
    public ICollection<ClassifierReference> Classifiers { get; set; } = new List<ClassifierReference>();
    public PurposeReference? Purpose { get; set; }
    public string? Notation { get; set; }
    public string? Symbol { get; set; }
    public Aspect Aspect { get; set; }

    public virtual ICollection<BlockTerminalTypeReference> BlockTerminals { get; set; } = null!;
    public virtual ICollection<BlockAttributeTypeReference> BlockAttributes { get; set; } = null!;
    //public virtual List<SelectedAttributePredefinedLibDm> SelectedAttributePredefined { get; set; }

    /*#region IVersionable

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
        var blockAttributeDms = new List<BlockAttributeTypeReference>();

        attributeAmIds.AddRange(other.Attributes ?? new List<string>());
        blockAttributeDms.AddRange(BlockAttributes ?? new List<BlockAttributeTypeReference>());

        if (blockAttributeDms.Select(y => y.AttributeId).Any(id => attributeAmIds.All(x => x != id)))
            validation.AddNotAllowToChange(nameof(BlockAttributes), "It is not allowed to remove or change attributes");

        //Terminals
        BlockTerminals ??= new List<BlockTerminalTypeReference>();
        other.BlockTerminals ??= new List<BlockTerminalLibAm>();
        var otherTerminals = other.BlockTerminals.Select(x => (x.TerminalId, x.Direction));
        if (BlockTerminals.Select(y => (y.TerminalId, y.Direction)).Any(identifier => otherTerminals.Select(x => x).All(x => x != identifier)))
        {
            validation.AddNotAllowToChange(nameof(BlockTerminals), "It is not allowed to remove terminals");
        }

        foreach (var terminal in other.BlockTerminals)
        {
            if (!BlockTerminals.Select(x => (x.TerminalId, x.Direction)).Contains((terminal.TerminalId, terminal.Direction))) continue;

            var current = BlockTerminals.FirstOrDefault(x => x.TerminalId == terminal.TerminalId && x.Direction == terminal.Direction);
            if (terminal.MaxCount != 0 && current?.MaxCount > terminal.MaxCount)
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
        var blockAttributeDms = new List<BlockAttributeTypeReference>();

        attributeAmIds.AddRange(other.Attributes ?? new List<string>());
        blockAttributeDms.AddRange(BlockAttributes ?? new List<BlockAttributeTypeReference>());

        if (!blockAttributeDms.Select(x => x.AttributeId).Order().SequenceEqual(attributeAmIds.Order()))
        {
            major = true;
        }

        // Block Terminals
        BlockTerminals ??= new List<BlockTerminalTypeReference>();
        other.BlockTerminals ??= new List<BlockTerminalLibAm>();
        var otherTerminals = other.BlockTerminals.Select(x => (x.TerminalId, x.Direction, MaxCount: x.MaxCount == 0 ? int.MaxValue : x.MaxCount)).OrderBy(x => x.TerminalId).ThenBy(x => x.Direction).ThenBy(x => x.MaxCount);
        if (!BlockTerminals.Select(x => (x.TerminalId, x.Direction, x.MaxCount)).OrderBy(x => x.TerminalId).ThenBy(x => x.Direction).ThenBy(x => x.MaxCount).SequenceEqual(otherTerminals))
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
            ObjectType = nameof(BlockType),
            ObjectName = Name,
            ObjectVersion = Version,
            LogType = logType,
            LogTypeValue = logTypeValue,
            Created = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }

    #endregion ILogable

    public bool Equals(BlockType other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        if (Name != other.Name || Aspect != other.Aspect || RdsId != other.RdsId || PurposeName != other.PurposeName || CompanyId != other.CompanyId || TypeReference != other.TypeReference || Description != other.Description || Symbol != other.Symbol)
            return false;

        // Block Attributes
        BlockAttributes ??= new List<BlockAttributeTypeReference>();
        other.BlockAttributes ??= new List<BlockAttributeTypeReference>();
        if (!BlockAttributes.Select(x => x.AttributeId).Order().SequenceEqual(other.BlockAttributes.Select(x => x.AttributeId).Order()))
        {
            return false;
        }

        // Block Terminals
        BlockTerminals ??= new List<BlockTerminalTypeReference>();
        other.BlockTerminals ??= new List<BlockTerminalTypeReference>();
        if (!BlockTerminals.Select(x => (x.TerminalId, x.Direction)).Order()
                .SequenceEqual(other.BlockTerminals.Select(x => (x.TerminalId, x.Direction)).Order()))
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
        return Equals((BlockType) obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }*/
}