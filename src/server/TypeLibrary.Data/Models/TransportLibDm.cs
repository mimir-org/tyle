using System;
using System.Collections.Generic;
using System.Linq;
using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Newtonsoft.Json;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models
{
    public class TransportLibDm : IVersionable<TransportLibAm>, IVersionObject, ILogable
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public TransportLibDm Parent { get; set; }
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
        public virtual ICollection<TransportLibDm> Children { get; set; }
        public virtual ICollection<AttributeLibDm> Attributes { get; set; }

        #region IVersionable

        public Validation HasIllegalChanges(TransportLibAm other)
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

            Attributes ??= new List<AttributeLibDm>();
            other.AttributeIdList ??= new List<string>();
            if (Attributes.Select(y => y.Id).Any(id => other.AttributeIdList.All(x => x != id)))
            {
                validation.AddNotAllowToChange(nameof(Attributes), "It is not allowed to remove items from attributes");
            }

            validation.IsValid = !validation.Result.Any();
            return validation;
        }

        public VersionStatus CalculateVersionStatus(TransportLibAm other)
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

            // Attributes
            Attributes ??= new List<AttributeLibDm>();
            other.AttributeIdList ??= new List<string>();
            if (!Attributes.Select(x => x.Id).SequenceEqual(other.AttributeIdList))
                major = true;

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
                ObjectType = nameof(TransportLibDm),
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