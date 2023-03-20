using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.Authentication.Models.Domain;

public class MimirorgHook
{
    public int Id { get; set; }
    public MimirorgCompany Company { get; set; }
    public int CompanyId { get; set; }
    public CacheKey Key { get; set; }
    public string Iri { get; set; }
}