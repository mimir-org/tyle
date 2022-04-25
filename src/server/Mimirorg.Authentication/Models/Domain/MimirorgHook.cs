using Mimirorg.Authentication.Models.Content;
using Mimirorg.Common.Enums;

namespace Mimirorg.Authentication.Models.Domain
{
    public class MimirorgHook
    {
        public int Id { get; set; }
        public MimirorgCompany Company { get; set; }
        public int CompanyId { get; set; }
        public CacheKey Key { get; set; }
        public string Iri { get; set; }

        public MimirorgHookCm ToContentModel()
        {
            return new MimirorgHookCm
            {
                Id = Id,
                CompanyId = CompanyId,
                Company = Company.ToContentModel(),
                Key = Key,
                Iri = Iri
            };
        }
    }
}
