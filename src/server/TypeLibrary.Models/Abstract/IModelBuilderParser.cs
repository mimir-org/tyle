using System.Threading.Tasks;
using TypeLibrary.Models.Application;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Models.Abstract
{
    public interface IModelBuilderParser : IModuleInterface
    {
        FileFormat GetFileFormat();
        Task<byte[]> SerializeProject(Project project);
        Task<Project> DeserializeProject(byte[] data);
        Task<ProjectAm> DeserializeProjectAm(byte[] data);
    }
}
