using TypeLibrary.Models.Abstract;
using TypeLibrary.Models.Data;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Application
{
    public class Module
    {
        public ModuleDescription ModuleDescription { get; set; }
        public IModuleInterface Instance { get; set; }
        public ModuleType ModuleType { get; set; }
    }
}
