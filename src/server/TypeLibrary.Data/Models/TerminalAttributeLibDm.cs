namespace TypeLibrary.Data.Models
{
    public class TerminalAttributeLibDm
    {
        public int Id { get; set; }
        public int TerminalId { get; set; }
        public TerminalLibDm Terminal { get; set; }
        public int AttributeId { get; set; }
        public AttributeLibDm Attribute { get; set; }
    }
}