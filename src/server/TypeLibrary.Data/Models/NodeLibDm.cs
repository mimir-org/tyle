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
    /// <summary>
    /// Node domain model
    /// </summary>
    public class NodeLibDm : IVersionable<NodeLibAm>
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public NodeLibDm Parent { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string FirstVersionId { get; set; }
        public string Iri { get; set; }
        public string TypeReferences { get; set; }
        public string RdsCode { get; set; }
        public string RdsName { get; set; }
        public string PurposeName { get; set; }
        public Aspect Aspect { get; set; }
        public State State { get; set; }
        public int CompanyId { get; set; }
        public string Symbol { get; set; }
        public virtual List<SelectedAttributePredefinedLibDm> SelectedAttributePredefined { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<NodeLibDm> Children { get; set; }
        public virtual ICollection<NodeTerminalLibDm> NodeTerminals { get; set; }
        public virtual ICollection<AttributeLibDm> Attributes { get; set; }
        public virtual ICollection<SimpleLibDm> Simples { get; set; }

        #region Versionable

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

            if (Simples != null && other.SimpleIdList != null)
            {
                if (Simples.Select(y => y.Id).Any(id => other.SimpleIdList.All(x => x != id)))
                {
                    validation.AddNotAllowToChange(nameof(Simples), "It is not allowed to remove items from simples");
                }
            }

            if (Attributes != null && other.AttributeIdList != null)
            {
                if (Attributes.Select(y => y.Id).Any(id => other.AttributeIdList.All(x => x != id)))
                {
                    validation.AddNotAllowToChange(nameof(Attributes), "It is not allowed to remove items from attributes");
                }
            }

            if (NodeTerminals != null && other.NodeTerminals != null)
            {
                if (NodeTerminals.Select(y => y.Id).Any(id => other.NodeTerminals.Select(x => x.Id).All(x => x != id)))
                {
                    validation.AddNotAllowToChange(nameof(NodeTerminals), "It is not allowed to remove items from terminals");
                }
            }

            if (SelectedAttributePredefined != null && other.SelectedAttributePredefined != null)
            {
                if (SelectedAttributePredefined.Select(y => y.Key).Any(key => other.SelectedAttributePredefined.Select(x => x.Key).All(x => x != key)))
                {
                    validation.AddNotAllowToChange(nameof(SelectedAttributePredefined), "It is not allowed to remove items from predefined attributes");
                }
            }

            if (ParentId != other.ParentId)
                validation.AddNotAllowToChange(nameof(ParentId));

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

            if (Simples != null && other.SimpleIdList != null)
            {
                if (!Simples.Select(x => x.Id).SequenceEqual(other.SimpleIdList))
                    major = true;
            }

            if (Attributes != null && other.AttributeIdList != null)
            {
                if (!Attributes.Select(x => x.Id).SequenceEqual(other.AttributeIdList))
                    major = true;
            }

            if (NodeTerminals != null && other.NodeTerminals != null)
            {
                if (!NodeTerminals.Select(x => x.Id).SequenceEqual(other.NodeTerminals.Select(x => x.Id)))
                    major = true;
            }

            if (SelectedAttributePredefined != null && other.SelectedAttributePredefined != null)
            {
                if (!SelectedAttributePredefined.Select(x => x.Key).SequenceEqual(other.SelectedAttributePredefined.Select(x => x.Key)))
                    major = true;
            }

            if (Description != other.Description)
                minor = true;

            if (Symbol != other.Symbol)
                minor = true;

            ICollection<TypeReferenceAm> references = null;

            if (!string.IsNullOrEmpty(TypeReferences))
                references = JsonConvert.DeserializeObject<ICollection<TypeReferenceAm>>(TypeReferences);

            if (references != null && references.SequenceEqual(other.TypeReferences))
                minor = true;

            if (references == null && other.TypeReferences != null)
                minor = true;


            return major ? VersionStatus.Major : minor ? VersionStatus.Minor : VersionStatus.NoChange;
        }

        #endregion
    }
}