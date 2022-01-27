﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TypeLibrary.Models.Application
{
    public class SimpleTypeAm
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }

        public ICollection<string> AttributeStringList { get; set; }

        [JsonIgnore]
        public string Key => $"{Name}";
    }
}
