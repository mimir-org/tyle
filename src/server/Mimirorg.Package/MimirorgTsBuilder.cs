using System.Reflection;
using TypeScriptBuilder;

namespace Mimirorg.Package
{
    public static class MimirorgTsBuilder
    {
        public static void CreateTypeScriptFiles()
        {
            try
            {
                CreateTypeScriptFile(new List<string>() { "Mimirorg.TypeLibrary.Models.Application", "Mimirorg.TypeLibrary.Models.Client" }, "template/index.ts");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void CreateTypeScriptFile(List<string> nameSpaces, string name)
        {
            var ts = new TypeScriptGenerator(new TypeScriptGeneratorOptions
            {
                IgnoreNamespaces = true,
                EmitIinInterface = false,
            });

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (path == null)
                return;

            var assemblies = Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFile).ToList();

            foreach (var types in assemblies.Select(assembly =>
                         assembly.GetTypes().Where(x => x.IsClass && !string.IsNullOrEmpty(x.Namespace) && nameSpaces.Contains(x.Namespace))))
            {
                foreach (var type in types)
                {
                    ts.AddCSType(type);
                }
            }


            ts.Store(name);
        }
    }
}