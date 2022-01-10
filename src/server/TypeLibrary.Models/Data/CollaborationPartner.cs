using System.Collections.Generic;

namespace TypeLibrary.Models.Data
{
    public class CollaborationPartner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public bool Current { get; set; }
        public ICollection<string> Iris { get; set; }
    }
}
