using System.Text;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Mimirorg.Common.Extensions
{
    public static class FileExtensions
    {
        public static byte[] Serialize(this object value)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            serializerSettings.Converters.Add(new StringEnumConverter());
            var result = JsonConvert.SerializeObject(value, serializerSettings);
            return Encoding.UTF8.GetBytes(result);

        }

        public static T Deserialize<T>(this byte[] value) where T: class, new()
        {
            if (value == null)
                throw new ModelBuilderNullReferenceException("The file byte array is null");

            var valueAsString = Encoding.UTF8.GetString(value, 0, value.Length);

            return JsonConvert.DeserializeObject<T>(valueAsString);
        }

        public static bool ValidateJsonFile(this IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension.ToLower() == ".json");
        }

        public static bool ValidateNtFile(this IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension.ToLower() == ".nt");
        }

    }
}
