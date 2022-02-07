namespace Mimirorg.TypeLibrary.Models.Data
{
    public class InterfaceLibDm : LibraryTypeLibDm
    {
        public string TerminalId { get; set; }
        public TerminalLibDm Terminal { get; set; }
        public ICollection<AttributeLibDm> Attributes { get; set; }
    }
}
