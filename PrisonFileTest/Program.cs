using System;
using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile;

namespace PrisonFileTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.BufferHeight = 10000;

            MyConsole.UseConsole = false;
            MyConsole.Name = "Prison File Test";

            Console.WriteLine("Parsing .prison file");
            PrisonFile prisonFile = new PrisonFile(@"C:\Users\Keith\AppData\Local\Introversion\Prison Architect\saves\test2.prison");

            Console.WriteLine("Finished!");
            Console.WriteLine();
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }
    }
}