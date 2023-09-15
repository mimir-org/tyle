namespace Tyle.Application.Blocks.Views;

public class TerminalTypeReferenceView : TerminalTypeView
{
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
    public Direction Direction { get; set; }
}