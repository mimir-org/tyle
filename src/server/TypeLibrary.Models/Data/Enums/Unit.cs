﻿using System.Collections.Generic;
using Newtonsoft.Json;
using TypeLibrary.Models.Data.TypeEditor;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Data.Enums
{
    public class Unit
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }

        private const string InternalType = "Mb.Models.Data.Enums.Unit";

        [JsonIgnore]
        public virtual string Key => $"{Name}-{InternalType}";

        [JsonIgnore]
        public virtual ICollection<AttributeType> AttributeTypes { get; set; }
    }
}