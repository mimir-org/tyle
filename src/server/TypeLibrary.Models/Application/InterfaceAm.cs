using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TypeLibrary.Models.Extensions;
using Attribute = TypeLibrary.Models.Data.Attribute;

namespace TypeLibrary.Models.Application
{
    public class InterfaceAm
    {
        public string Id { get; set; }
        public string Iri { get; set; }
        public string Domain => Id.ResolveDomain();
        public string Version { get; set; }
        public string Rds { get; set; }

        [Required]
        public string Name { get; set; }

        public string Label { get; set; }
        public string Description { get; set; }

        //[Required]
        public string StatusId { get; set; }

        public string SemanticReference { get; set; }
        public string InputTerminalId { get; set; }
        public TerminalAm InputTerminal { get; set; }
        public string OutputTerminalId { get; set; }
        public TerminalAm OutputTerminal { get; set; }
        public ICollection<Attribute> Attributes { get; set; }
        
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }

        public string LibraryTypeId { get; set; }
    }
}
