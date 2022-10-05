using System.ComponentModel.DataAnnotations;
using Mimirorg.Common.Attributes;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using TypeScriptBuilder;

namespace Mimirorg.TypeLibrary.Models.Application
{
    /// <summary>
    /// Object used to create or update a transport
    /// </summary>
    public class TransportLibAm
    {
        /// <summary>
        /// The name of the transport
        /// </summary>
        /// <remarks>
        /// The name is not allowed to change
        /// </remarks>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The RDS name of the transport
        /// </summary>
        /// <remarks>
        /// The RDS name is not allowed to change
        /// </remarks>
        [Required]
        public string RdsName { get; set; }

        /// <summary>
        /// The RDS code of the transport
        /// </summary>
        /// <remarks>
        /// The RDS code is not allowed to change
        /// </remarks>
        [Required]
        public string RdsCode { get; set; }

        /// <summary>
        /// The purpose name of the transport
        /// </summary>
        /// <remarks>
        /// A purpose name change will trigger a minor version increase
        /// </remarks>
        [Required]
        public string PurposeName { get; set; }

        /// <summary>
        /// The aspect of the transport
        /// </summary>
        /// <remarks>
        /// The aspect is not allowed to change
        /// </remarks>
        [Required]
        public Aspect Aspect { get; set; }

        /// <summary>
        /// The owner of the transport type
        /// </summary>
        /// <remarks>
        /// A company id change, will trigger a minor version increase
        /// </remarks>
        [Display(Name = "CompanyId")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than 0")]
        public int CompanyId { get; set; }

        /// <summary>
        /// The connected terminal type
        /// </summary>
        /// <remarks>
        /// The terminal id is not allowed to change
        /// </remarks>
        [Required]
        public string TerminalId { get; set; }

        /// <summary>
        /// A list of connected attributes
        /// </summary>
        /// <remarks>
        /// It is not allowed to remove attributes
        /// Adding attributes generates a major increase
        /// </remarks>
        public ICollection<string> AttributeIdList { get; set; }

        /// <summary>
        /// A list of references to other ontologies
        /// </summary>
        /// <remarks>
        /// It is allowed to change the list. Changing will generate a minor increase
        /// </remarks>
        public ICollection<TypeReferenceAm> TypeReferences { get; set; }

        /// <summary>
        /// The description of the transport type
        /// </summary>
        /// <remarks>
        /// A description change, will trigger a minor version increase
        /// </remarks>
        public string Description { get; set; }

        /// <summary>
        /// The parent id for the transport type
        /// </summary>
        /// <remarks>
        /// The parent id is not allowed to change
        /// </remarks>
        public string ParentId { get; set; }

        /// <summary>
        /// The transport version
        /// </summary>
        /// <remarks>
        /// The value will be set on server on create
        /// </remarks>
        [Double]
        public string Version { get; set; }

        /// <summary>
        /// The transport id
        /// </summary>
        /// <remarks>
        /// The value will be set on server
        /// </remarks>
        [TSExclude]
        public string Id => $"{Name}-{RdsCode}-{Aspect}-{Version}".CreateMd5();
    }
}