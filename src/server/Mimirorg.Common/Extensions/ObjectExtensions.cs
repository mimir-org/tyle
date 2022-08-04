using System.ComponentModel.DataAnnotations;
using Mimirorg.Common.Models;
using Newtonsoft.Json;

namespace Mimirorg.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static Validation ValidateObject(this object obj)
        {
            var validation = new Validation();

            var context = new ValidationContext(obj, null, null);
            var results = new List<ValidationResult>();

            validation.IsValid = Validator.TryValidateObject(obj, context, results, true);
            if (validation.IsValid)
                return validation;

            validation.Result = results;
            return validation;
        }

        public static IEnumerable<Validation> ValidateObjects(this IEnumerable<object> objects)
        {
            if (objects == null)
                yield break;

            foreach (var obj in objects)
            {
                if(obj == null)
                    continue;

                yield return obj.ValidateObject();
            }
        }

        public static T DeepCopy<T>(this T self)
        {
            var serialized = JsonConvert.SerializeObject(self);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public static object GetPropValue(this object obj, string name)
        {
            foreach (var part in name.Split('.'))
            {
                if (obj == null) { return null; }

                var type = obj.GetType();
                var info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static T GetPropValue<T>(this object obj, string name)
        {
            var value = GetPropValue(obj, name);
            if (value == null) { return default; }
            return (T) value;
        }
    }
}