namespace Tyle.Core.Common;

public abstract class HasCardinality
{
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
}