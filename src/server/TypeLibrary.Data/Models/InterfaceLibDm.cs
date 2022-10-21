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
    public class InterfaceLibDm : IVersionable<InterfaceLibAm>, IVersionObject, ILogable
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public InterfaceLibDm Parent { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string FirstVersionId { get; set; }
        public string Iri { get; set; }
        public string TypeReferences { get; set; }
        public string RdsCode { get; set; }
        public string RdsName { get; set; }
        public string PurposeName { get; set; }
        public Aspect Aspect { get; set; }
        public int CompanyId { get; set; }
        public string TerminalId { get; set; }
        public TerminalLibDm Terminal { get; set; }
        public string Description { get; set; }
        public State State { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public virtual ICollection<InterfaceLibDm> Children { get; set; }
        public string Attributes { get; set; }

        #region IVersionable

        public Validation HasIllegalChanges(InterfaceLibAm other)
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

            if (TerminalId != other.TerminalId)
                validation.AddNotAllowToChange(nameof(TerminalId));

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

            validation.IsValid = !validation.Result.Any();
            return validation;
        }

        public VersionStatus CalculateVersionStatus(InterfaceLibAm other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            var minor = false;
            var major = false;

            if (PurposeName != other.PurposeName)
                minor = true;

            if (CompanyId != other.CompanyId)
                minor = true;

            if (Description != other.Description)
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
                ObjectType = nameof(InterfaceLibDm),
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