using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TypeLibrary.Models.Enums;
using TypeLibrary.Models.Models.Client;

namespace TypeLibrary.Models.Models.Application
{
    public class TypeAm : IValidatableObject
    {
        [Required]
        public string Name { get; set; }

        public string Domain { get; set; }

        public string Description { get; set; }

        [EnumDataType(typeof(Aspect))]
        public Aspect Aspect { get; set; }

        [EnumDataType(typeof(ObjectType))]
        public ObjectType ObjectType { get; set; }

        public string SemanticReference { get; set; }

        [Required]
        public string RdsId { get; set; }

        [Required] 
        public string StatusId { get; set; } = "4590637F39B6BA6F39C74293BE9138DF";

        [Required]
        public string Purpose { get; set; }

        public ICollection<TerminalCm> TerminalTypes { get; set; }
        public string SymbolId { get; set; }
        public ICollection<string> AttributeStringList { get; set; }
        public string LocationType { get; set; }
        public ICollection<PredefinedAttributeCm> PredefinedAttributes { get; set; }
        public string TerminalTypeId { get; set; }
        public ICollection<string> SimpleTypes { get; set; }
        public ICollection<CategoryAm> Categories { get; set; }

        [JsonIgnore]
        public string Key => $"{Name}-{RdsId}-{Aspect}-{Version}";

        public string Version { get; set; }
        public string TypeId { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((ObjectType == ObjectType.Interface || ObjectType == ObjectType.Transport) && string.IsNullOrEmpty(TerminalTypeId))
            {
                yield return new ValidationResult(

                    "If object typeDm is of typeDm interface or transport, TerminalId must be set.",
                    new List<string> { "TerminalId" }
                );
            }
        }
    }
}
