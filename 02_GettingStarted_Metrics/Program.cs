using System;

namespace GettingStarted_Metrics
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectId = "aspnetcoreissue";

            Console.WriteLine("Each . represents unit of work that's done and instrumented");
            Console.WriteLine($"Navigate to https://console.cloud.google.com/monitoring?project={projectId}!");
            StackdriverSample.Run(projectId);

            Console.Read();
        }
    }
}
