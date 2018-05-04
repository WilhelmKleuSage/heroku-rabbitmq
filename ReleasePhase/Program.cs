using System;
using System.Collections;
using TestLibrary;

namespace ReleasePhase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running release phase");

            Console.WriteLine("Environment variables:");
            foreach (DictionaryEntry env in Environment.GetEnvironmentVariables())
            {
                Console.WriteLine("{0}={1}", env.Key, env.Value);
            }

            TestClass.PrintSomething("Test 123");
        }
    }
}
