using System.ComponentModel.DataAnnotations;
using Mimirorg.Common.Attributes;
using Mimirorg.TypeLibrary.Extensions;
using TypeScriptBuilder;
// ReSharper disable InconsistentNaming

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class TerminalLibAm
    {
        /// <summary>
        /// The name of the terminal
        /// </summary>
        /// <remarks>
        /// The name is not allowed to change
        /// </remarks>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The parent terminal id
        /// </summary>
        /// <remarks>
        /// The parent id is not allowed to change
        /// </remarks>
        public string ParentId { get; set; }

        /// <summary>
        /// A list of references to other ontologies
        /// </summary>
        /// <remarks>
        /// It is allowed to change the list. Changing will generate a minor increase
        /// </remarks>
        public ICollection<TypeReferenceAm> TypeReferences { get; set; }

        /// <summary>
        /// The color of the terminal
        /// </summary>
        /// <remarks>
        /// It is allowed to change the color. Changing will generate a minor increase
        /// </remarks>
        [Required]
        public string Color { get; set; }

        /// <summary>
        /// A description of the terminal
        /// </summary>
        /// <remarks>
        /// It is allowed to change the description. Changing will generate a minor increase
        /// </remarks>
        public string Description { get; set; }

        /// <summary>
        /// A list of connected attributes
        /// </summary>
        /// <remarks>
        /// It is not allowed to remove attributes
        /// Adding attributes generates a major increase
        /// </remarks>
        public ICollection<string> AttributeIdList { get; set; }

        /// <summary>
        /// The owner of the terminal type
        /// </summary>
        /// <remarks>
        /// A company id change, will trigger a minor version increase
        /// </remarks>
        [Display(Name = "CompanyId")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than 0")]
        public int CompanyId { get; set; }

        /// <summary>
        /// The terminal version
        /// </summary>
        /// <remarks>
        /// The value will be set on server
        /// </remarks>
        [TSExclude]
        [Double]
        public string Version { get; set; }

        /// <summary>
        /// The terminal id
        /// </summary>
        /// <remarks>
        /// The value will be set on server
        /// </remarks>
        [TSExclude]
        public string Id => $"{Name}-{Version}".CreateMd5();
    }
}