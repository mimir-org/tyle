using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Application;

public class LogLibAm
{
    [Required]
    public int ObjectId { get; set; }
    [Required]
    public int ObjectFirstVersionId { get; set; }
    [Required]
    public string ObjectName { get; set; }
    [Required]
    public string ObjectVersion { get; set; }
    [Required]
    public string ObjectType { get; set; }
    [Required]
    public LogType LogType { get; set; }
    [Required]
    public string LogTypeValue { get; set; }

    public string Comment { get; set; }
}