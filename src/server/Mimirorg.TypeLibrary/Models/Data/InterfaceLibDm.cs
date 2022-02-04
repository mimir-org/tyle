namespace Mimirorg.TypeLibrary.Models.Data
{
    public class InterfaceLibDm : TypeLibDm
    {
        public string TerminalId { get; set; }
        public TerminalLibDm TerminalDm { get; set; }
        public ICollection<AttributeLibDm> AttributeList { get; set; }
    }
}
