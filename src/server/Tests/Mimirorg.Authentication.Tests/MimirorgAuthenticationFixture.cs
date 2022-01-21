using System;

namespace Mimirorg.Authentication.Tests
{
    public class MimirorgAuthenticationFixture : IDisposable
    {
        public MimirorgAuthenticationFixture()
        {
            Console.WriteLine("Inside SetUp Constructor");
        }

        public void Dispose()
        {
            Console.WriteLine("Inside CleanUp or Dispose method");
        }
    }
}
