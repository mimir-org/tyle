using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Contracts;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using Newtonsoft.Json;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models
{
    public class AttributeLibDm : ILibraryType, IVersionable<AttributeLibAm>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string FirstVersionId { get; set; }
        public string Iri { get; set; }
        public string TypeReferences { get; set; }

        public string AttributeQualifier { get; set; }
        public string AttributeSource { get; set; }
        public string AttributeCondition { get; set; }
        public string AttributeFormat { get; set; }

        public int CompanyId { get; set; }
        public Aspect Aspect { get; set; }
        public Discipline Discipline { get; set; }
        public Select Select { get; set; }
        public string SelectValuesString { get; set; }
        public string Units { get; set; }
        public State State { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        public virtual HashSet<string> Tags { get; set; }

        [NotMapped]
        public ICollection<string> SelectValues => string.IsNullOrEmpty(SelectValuesString) ? null : SelectValuesString.ConvertToArray();

        public virtual ICollection<TerminalLibDm> Terminals { get; set; }
        public virtual ICollection<InterfaceLibDm> Interfaces { get; set; }
        public virtual ICollection<NodeLibDm> Nodes { get; set; }
        public virtual ICollection<SimpleLibDm> Simple { get; set; }
        public virtual ICollection<TransportLibDm> Transports { get; set; }

        public Validation HasIllegalChanges(AttributeLibAm other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            var validation = new Validation();

            if (Name != other.Name)
                validation.AddNotAllowToChange(nameof(Name));

            if (AttributeQualifier != other.AttributeQualifier)
                validation.AddNotAllowToChange(nameof(AttributeQualifier));

            if (AttributeSource != other.AttributeSource)
                validation.AddNotAllowToChange(nameof(AttributeSource));

            if (AttributeCondition != other.AttributeCondition)
                validation.AddNotAllowToChange(nameof(AttributeCondition));

            if (AttributeFormat != other.AttributeFormat)
                validation.AddNotAllowToChange(nameof(AttributeFormat));

            if (Aspect != other.Aspect)
                validation.AddNotAllowToChange(nameof(Aspect));

            if (Discipline != other.Discipline)
                validation.AddNotAllowToChange(nameof(Discipline));

            var unitsDm = Units.ConvertToObject<ICollection<UnitLibCm>>().Select(x => x.Id);

            if (unitsDm.Select(y => y).Any(id => other.UnitIdList.Select(x => x).All(x => x != id)))
                validation.AddNotAllowToChange(nameof(Units), "It is not allowed to remove units from an attribute");

            validation.IsValid = !validation.Result.Any();
            return validation;
        }

        public VersionStatus CalculateVersionStatus(AttributeLibAm other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            var minor = false;
            var major = false;

            //CompanyId
            if (CompanyId != other.CompanyId)
                minor = true;

            //Select
            if (Select != other.Select)
                minor = true;

            //SelectValuesString
            if (SelectValuesString != other.SelectValues.ToString())
                minor = true;

            // Type-references
            var references = string.IsNullOrWhiteSpace(TypeReferences)
                ? new List<TypeReferenceAm>()
                : JsonConvert.DeserializeObject<ICollection<TypeReferenceAm>>(TypeReferences) ?? new List<TypeReferenceAm>();
            other.TypeReferences ??= new List<TypeReferenceAm>();
            if (!references.SequenceEqual(other.TypeReferences))
                minor = true;

            //Units
            if (Units.ConvertToObject<ICollection<UnitLibCm>>().Count < other.UnitIdList.Count)
                major = true;

            return major ? VersionStatus.Major : minor ? VersionStatus.Minor : VersionStatus.NoChange;
        }
    }
}