using Mimirorg.Authentication.Enums;

namespace Mimirorg.Authentication.Models.Client;

public class MimirorgHookCm
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public MimirorgCompanyCm Company { get; set; }
    public CacheKey Key { get; set; }
    public string Iri { get; set; }
}