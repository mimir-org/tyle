namespace Mimirorg.TypeLibrary.Models.Data
{
    public class TransportLibDm : LibraryTypeLibDm
    {
        public string TerminalId { get; set; }
        public TerminalLibDm Terminal { get; set; }
        public ICollection<AttributeLibDm> Attributes { get; set; }
    }
}
