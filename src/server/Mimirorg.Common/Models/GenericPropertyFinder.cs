namespace Mimirorg.Common.Models
{
    public class GenericPropertyFinder<TModel> where TModel : class
    {
        public IEnumerable<string> Get(TModel model)
        {
            var type = model.GetType();
            var propertyInfos = type.GetProperties();

            foreach (var property in propertyInfos)
            {
                yield return property.Name;
            }
        }
    }
}