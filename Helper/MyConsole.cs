using System;
using System.IO;

namespace PrisonArchitect.Helper
{
    public class MyConsole
    {
        public static bool UseConsole = false;
        public static string Output = "";
        private static string _name = "";

        public static string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                if (UseConsole) Console.Title = _name;
            }
        }

        public static void WriteLogFile()
        {
            string file = Name + ".log";

            #region Create File

            DirectoryInfo directoryInfo = (new FileInfo(file)).Directory;
            if (directoryInfo != null)
            {
                if (!Directory.Exists(directoryInfo.FullName))
                    Directory.CreateDirectory(directoryInfo.FullName);
            }
            if (!File.Exists(file))
            {
                FileStream fstream = File.Create(file);
                fstream.Close();
            }

            #endregion Create File

            StreamWriter sw = new StreamWriter(file, false);
            sw.Write(Output);
            sw.Close();
        }

        public static string ReadLine() { return UseConsole ? Console.ReadLine() : null; }

        public static void Write(string line = "")
        {
            if (UseConsole) Console.Write(line);
            Output += line;
        }

        public static void WriteLine(string line = "", bool clear = false)
        {
            if (UseConsole) Console.WriteLine(line);
            Output += line + Environment.NewLine;
        }
    }
}