using System;
using System.Collections.Generic;
using System.Linq;
using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Newtonsoft.Json;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models
{
    /// <summary>
    /// Node domain model
    /// </summary>
    public class NodeLibDm : IVersionable<NodeLibAm>, IVersionObject, ILogable
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public NodeLibDm Parent { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public int FirstVersionId { get; set; }
        public string Iri { get; set; }
        public string TypeReferences { get; set; }
        public string RdsCode { get; set; }
        public string RdsName { get; set; }
        public string PurposeName { get; set; }
        public Aspect Aspect { get; set; }
        public int CompanyId { get; set; }
        public string Symbol { get; set; }
        public virtual List<SelectedAttributePredefinedLibDm> SelectedAttributePredefined { get; set; }
        public string Description { get; set; }
        public State State { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public virtual ICollection<NodeLibDm> Children { get; set; }
        public virtual ICollection<NodeTerminalLibDm> NodeTerminals { get; set; }
        public ICollection<NodeAttributeLibDm> NodeAttributes { get; set; }

        #region IVersionable

        public Validation HasIllegalChanges(NodeLibAm other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            var validation = new Validation();

            if (Name != other.Name)
                validation.AddNotAllowToChange(nameof(Name));

            if (RdsName != other.RdsName)
                validation.AddNotAllowToChange(nameof(RdsName));

            if (RdsCode != other.RdsCode)
                validation.AddNotAllowToChange(nameof(RdsCode));

            if (Aspect != other.Aspect)
                validation.AddNotAllowToChange(nameof(Aspect));

            if (ParentId != other.ParentId)
                validation.AddNotAllowToChange(nameof(ParentId));

            //Attributes
            var attributeAms = new List<AttributeLibAm>();
            var attributeDms = new List<AttributeLibDm>();
            var attributeAmUnits = new List<UnitLibAm>();
            var attributeDmUnits = new List<UnitLibDm>();

            attributeAms.AddRange(other.Attributes ?? new List<AttributeLibAm>());
            attributeDms.AddRange(Attributes?.ConvertToObject<ICollection<AttributeLibDm>>() ?? new List<AttributeLibDm>());
            attributeAmUnits.AddRange(attributeAms.SelectMany(x => x.Units));
            attributeDmUnits.AddRange(attributeDms.SelectMany(x => x.Units));

            if (attributeDms.Select(y => y.Id).Any(id => attributeAms.Select(x => x.Id).All(x => x != id)))
                validation.AddNotAllowToChange(nameof(Attributes), "It is not allowed to remove or change attributes");

            if (attributeDmUnits.Select(y => y.Id).Any(id => attributeAmUnits.Select(x => x.Id).All(x => x != id)))
                validation.AddNotAllowToChange(nameof(Attributes), "It is not allowed to remove or change units from attributes");

            NodeTerminals ??= new List<NodeTerminalLibDm>();
            other.NodeTerminals ??= new List<NodeTerminalLibAm>();
            var otherTerminals = other.NodeTerminals.Select(x => (x.TerminalId, x.ConnectorDirection));
            if (NodeTerminals.Select(y => (y.TerminalId, y.ConnectorDirection)).Any(identifier => otherTerminals.Select(x => x).All(x => x != identifier)))
            {
                validation.AddNotAllowToChange(nameof(NodeTerminals), "It is not allowed to remove items from terminals");
            }

            SelectedAttributePredefined ??= new List<SelectedAttributePredefinedLibDm>();
            other.SelectedAttributePredefined ??= new List<SelectedAttributePredefinedLibAm>();
            if (SelectedAttributePredefined.Select(y => y.Key).Any(key => other.SelectedAttributePredefined.Select(x => x.Key).All(x => x != key)))
            {
                validation.AddNotAllowToChange(nameof(SelectedAttributePredefined), "It is not allowed to remove items from predefined attributes");
            }

            validation.IsValid = !validation.Result.Any();
            return validation;
        }

        public VersionStatus CalculateVersionStatus(NodeLibAm other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            var minor = false;
            var major = false;

            if (PurposeName != other.PurposeName)
                minor = true;

            if (CompanyId != other.CompanyId)
                minor = true;

            //Attributes
            var attributeAms = new List<AttributeLibAm>();
            var attributeDms = new List<AttributeLibDm>();
            var attributeAmUnits = new List<UnitLibAm>();
            var attributeDmUnits = new List<UnitLibDm>();

            attributeAms.AddRange(other.Attributes ?? new List<AttributeLibAm>());
            attributeDms.AddRange(Attributes?.ConvertToObject<ICollection<AttributeLibDm>>() ?? new List<AttributeLibDm>());
            attributeAmUnits.AddRange(attributeAms.SelectMany(x => x.Units));
            attributeDmUnits.AddRange(attributeDms.SelectMany(x => x.Units));

            if (!attributeDms.Select(x => x.Id).SequenceEqual(attributeAms.Select(x => x.Id)) ||
                !attributeDmUnits.Select(x => x.Id).SequenceEqual(attributeAmUnits.Select(x => x.Id)))
            {
                major = true;
            }

            // Type-references
            var references = string.IsNullOrWhiteSpace(TypeReferences)
                ? new List<TypeReferenceAm>()
                : JsonConvert.DeserializeObject<ICollection<TypeReferenceAm>>(TypeReferences) ?? new List<TypeReferenceAm>();
            other.TypeReferences ??= new List<TypeReferenceAm>();
            if (!references.SequenceEqual(other.TypeReferences))
                minor = true;

            // Node Terminals
            NodeTerminals ??= new List<NodeTerminalLibDm>();
            other.NodeTerminals ??= new List<NodeTerminalLibAm>();
            var otherTerminals = other.NodeTerminals.Select(x => (x.TerminalId, x.ConnectorDirection));
            if (!NodeTerminals.Select(x => (x.TerminalId, x.ConnectorDirection)).SequenceEqual(otherTerminals))
                major = true;

            // Attribute Predefined
            SelectedAttributePredefined ??= new List<SelectedAttributePredefinedLibDm>();
            other.SelectedAttributePredefined ??= new List<SelectedAttributePredefinedLibAm>();
            if (!SelectedAttributePredefined.Select(x => x.Key).SequenceEqual(other.SelectedAttributePredefined.Select(x => x.Key)))
                major = true;

            if (Description != other.Description)
                minor = true;

            if (Symbol != other.Symbol)
                minor = true;

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
                ObjectType = nameof(NodeLibDm),
                ObjectName = Name,
                ObjectVersion = Version,
                LogType = logType,
                LogTypeValue = logTypeValue,
                Comment = comment
            };
        }

        #endregion ILogable
    }
}