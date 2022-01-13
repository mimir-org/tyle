//using System.Collections.Generic;
//using Newtonsoft.Json;
//using TypeLibrary.Models.Enums;

//namespace TypeLibrary.Models.Data.Enums
//{
//    public class EnumBase
//    {
//        public string Id { get; set; }
//        public string Name { get; set; }
//        public Aspect Aspect { get; set; }
//        public string ParentId { get; set; }
//        public EnumBase Parent { get; set; }
//        public ICollection<EnumBase> Children { get; set; }

//        [JsonIgnore]
//        public string InternalType { get; internal set; }
//        public virtual string Description { get; set; }
//        public virtual string SemanticReference { get; set; }

//        public EnumBase()
//        {
//            InternalType = GetType().FullName;
//        }

//        [JsonIgnore]
//        public virtual string Key => string.IsNullOrEmpty(ParentId) ? $"{Name}-{InternalType}" : $"{Name}-{InternalType}-{ParentId}";
//    }
//}
