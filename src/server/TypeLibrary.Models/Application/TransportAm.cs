using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TypeLibrary.Models.Extensions;

namespace TypeLibrary.Models.Application
{
    public class TransportAm
    {
        public string Id { get; set; }
        public string Iri { get; set; }
        public string Domain => Id.ResolveDomain();
        public string Version { get; set; }
        [Required]
        public string Rds { get; set; }
        [Required]
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string StatusId { get; set; }
        public string SemanticReference { get; set; }
        public string InputTerminalId { get; set; }
        public TerminalAm InputTerminal { get; set; }
        public string OutputTerminalId { get; set; }
        public TerminalAm OutputTerminal { get; set; }
        public ICollection<AttributeAm> Attributes { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Created { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public string LibraryTypeId { get; set; }
    }
}
