using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TypeLibrary.Models.Const
{
    public static class DefaultSettings
    {
        public static JsonSerializerSettings SerializerSettings => GetDefaultSerializer();

        private static JsonSerializerSettings GetDefaultSerializer()
        {
            return new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
    }
}
