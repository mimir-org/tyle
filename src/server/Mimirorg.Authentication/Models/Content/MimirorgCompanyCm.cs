﻿using Newtonsoft.Json;

namespace Mimirorg.Authentication.Models.Content
{
    public class MimirorgCompanyCm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public MimirorgUserCm Manager { get; set; }

        [JsonIgnore]
        public string Secret { get; set; }
        public string Domain { get; set; }
        public ICollection<string> Iris { get; set; }
    }
}
