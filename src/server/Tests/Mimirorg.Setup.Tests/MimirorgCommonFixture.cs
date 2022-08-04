namespace Mimirorg.Setup.Tests
{
    public class MimirorgCommonFixture : IDisposable
    {
        public MimirorgCommonFixture()
        {
            Console.WriteLine("Inside SetUp Constructor");
        }

        public void Dispose()
        {
            Console.WriteLine("Inside CleanUp or Dispose method");
        }
    }
}