namespace Mimirorg.TypeLibrary.Models.Data
{
    public class AttributePredefinedLibDm
    {
        public string Key { get; set; }
        public virtual ICollection<string> Values { get; set; }
        public bool IsMultiSelect { get; set; }
    }
}