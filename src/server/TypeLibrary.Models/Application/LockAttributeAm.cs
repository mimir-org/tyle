using System;
using System.ComponentModel.DataAnnotations;

namespace TypeLibrary.Models.Application
{
    public class LockAttributeAm
    {
        [Required]
        public string Id { get; set; }
        
        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public string ProjectId { get; set; }

        public string IsLockedStatusBy { get; set; }
        public DateTime? IsLockedStatusDate { get; set; }
        public string NodeId { get; set; }
        public string TransportId { get; set; }
        public string InterfaceId { get; set; }
        public string CompositeId { get; set; }
        public string EdgeId { get; set; }
        public string TerminalId { get; set; }
    }
}