using System;
using System.Collections.Generic;
using System.Linq;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Newtonsoft.Json;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models
{
    public class TerminalLibDm : IVersionable<TerminalLibAm>
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public TerminalLibDm Parent { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string FirstVersionId { get; set; }
        public string Iri { get; set; }
        public string TypeReferences { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public int CompanyId { get; set; }
        public bool Deleted { get; set; }
        public ICollection<AttributeLibDm> Attributes { get; set; }
        public ICollection<TerminalLibDm> Children { get; set; }
        public ICollection<NodeTerminalLibDm> TerminalNodes { get; set; }
        public ICollection<InterfaceLibDm> Interfaces { get; set; }
        public ICollection<TransportLibDm> Transports { get; set; }

        #region Versionable

        public Validation HasIllegalChanges(TerminalLibAm other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            var validation = new Validation();

            if (Name != other.Name)
                validation.AddNotAllowToChange(nameof(Name));

            if (Attributes != null && other.AttributeIdList != null)
            {
                if (Attributes.Select(y => y.Id).Any(id => other.AttributeIdList.All(x => x != id)))
                {
                    validation.AddNotAllowToChange(nameof(Attributes), "It is not allowed to remove items from attributes");
                }
            }

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

            if (Description != other.Description)
                minor = true;

            if (Color != other.Color)
                minor = true;

            if (Attributes != null && other.AttributeIdList != null)
            {
                if (!Attributes.Select(x => x.Id).SequenceEqual(other.AttributeIdList))
                    major = true;
            }

            ICollection<TypeReferenceAm> references = null;

            if (!string.IsNullOrEmpty(TypeReferences))
                references = JsonConvert.DeserializeObject<ICollection<TypeReferenceAm>>(TypeReferences);

            if (references != null && !references.SequenceEqual(other.TypeReferences))
                minor = true;

            return major ? VersionStatus.Major : minor ? VersionStatus.Minor : VersionStatus.NoChange;

        }

        #endregion Versionable
    }
}