using Mimirorg.Common.Enums;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class ApprovalCm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ObjectType { get; set; }
        public State State { get; set; }
        public string StateName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}