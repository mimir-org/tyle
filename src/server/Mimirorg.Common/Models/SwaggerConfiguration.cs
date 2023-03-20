namespace Mimirorg.Common.Models;

public class SwaggerConfiguration
{
    public string Title { get; set; }
    public string Description { get; set; }
    public SwaggerContact Contact { get; set; }
    public IList<Scope> Scopes { get; set; }
    public Dictionary<string, string> ScopesDictionary => ConvertToDictionary();

    private Dictionary<string, string> ConvertToDictionary()
    {
        var dictionary = new Dictionary<string, string>();
        if (Scopes == null)
            return dictionary;


        foreach (var scope in Scopes)
        {
            dictionary.Add(scope.Name, scope.Description);
        }

        return dictionary;
    }
}