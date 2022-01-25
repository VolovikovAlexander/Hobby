using System;
using Forbidden_Hosts;

namespace Forbidden_Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var randomHosts = HostGenerator.CreateRandomHosts(500);

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
