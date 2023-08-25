using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application;

public class BlockAttributeLibAm
{
    [Required]
    public int MinCount { get; set; }


    public int? MaxCount { get; set; }

    [Required]
    public Guid AttributeId { get; set; }
}