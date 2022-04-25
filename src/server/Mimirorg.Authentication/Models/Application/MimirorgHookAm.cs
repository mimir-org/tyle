using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Enums;

namespace Mimirorg.Authentication.Models.Application
{
    public class MimirorgHookAm
    {
        public int CompanyId { get; set; }
        public CacheKey Key { get; set; }
        public string Iri { get; set; }

        public MimirorgHook ToDomainModel()
        {
            return new MimirorgHook
            {
                CompanyId = CompanyId,
                Key = Key,
                Iri = Iri
            };
        }
    }
}
