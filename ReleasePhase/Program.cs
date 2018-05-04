using System;
using System.Collections;
using System.IO;
using System.Net;
using TestLibrary;

namespace ReleasePhase
{
    class Program
    {
        private const string DownloadLinkKey = "DOWNLOAD_LINK";

        private static void TestRetrievalOfFile(string url)
        {
            using (var wc = new WebClient())
            {
                Console.WriteLine($"Downloading from {url}");
                wc.DownloadFile(new Uri(url), "current.txt");
                var content = File.ReadAllText("current.txt");
                Console.WriteLine($"File Content: {content}");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Running release phase");

            Console.WriteLine("Environment variables:");
            var variables = Environment.GetEnvironmentVariables();
            foreach (DictionaryEntry env in variables)
            {
                Console.WriteLine("{0}={1}", env.Key, env.Value);
            }

            TestClass.PrintSomething("Test 123");
            TestRetrievalOfFile(variables[DownloadLinkKey].ToString());
        }
    }
}
