using System;
using System.IO;
using PrisonArchitect.Helper;

namespace PrisonArchitect.PrisonFile
{
    public class PrisonFile
    {
        private readonly Block _block;
        private readonly PrisonFileParser _prisonFileParser = new PrisonFileParser();

        public PrisonFile(string filename)
        {
            FileName = filename;

            MyConsole.WriteLine(filename);
            MyConsole.WriteLine(new string('-', Console.BufferWidth - 1));

            // Read the file and display it line by line.
            StreamReader file = new StreamReader(filename);

            _prisonFileParser.Parse(file.ReadToEnd(), out _block);

            file.Close();

#if DEBUG
            _block.Print(true);
            MyConsole.WriteLogFile();
#endif
            MyConsole.WriteLine(new string('-', Console.BufferWidth - 1));
        }

        public string FileName { get; set; }
    }
}