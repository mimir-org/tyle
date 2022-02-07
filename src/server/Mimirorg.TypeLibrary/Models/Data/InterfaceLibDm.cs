namespace Mimirorg.TypeLibrary.Models.Data
{
    public class InterfaceLibDm : LibraryTypeLibDm
    {
        public string TerminalId { get; set; }
        public TerminalLibDm TerminalDm { get; set; }
        public ICollection<AttributeLibDm> AttributeList { get; set; }
    }
}
