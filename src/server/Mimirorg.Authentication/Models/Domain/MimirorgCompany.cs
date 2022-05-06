using Mimirorg.Authentication.Models.Content;

namespace Mimirorg.Authentication.Models.Domain
{
    public class MimirorgCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Secret { get; set; }

        public string ManagerId { get; set; }
        public MimirorgUser Manager { get; set; }

        public List<MimirorgHook> Hooks { get; set; }

        public MimirorgCompanyCm ToContentModel()
        {
            return new MimirorgCompanyCm
            {
                Id = Id,
                Name = Name,
                DisplayName = DisplayName,
                Description = Description,
                Manager = Manager?.ToContentModel(),
                Secret = Secret
            };
        }
    }
}