
namespace GettingStarted_Trace
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            ZipkinSample.Run("http://localhost:9411/api/v2/spans");
            Console.WriteLine("Inspect Traces at: http://localhost:9411");
        }
    }
}
