using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.BlockWrappers;

namespace PrisonArchitect.PrisonFile
{
    public class PrisonFile
    {
        public const string GameVersion = "alpha-22";

        #region Variables

        // ReSharper disable InconsistentNaming

        private readonly Block _block;

        public string Version { get { return _block.Variables["Version"].SafeParse<string>(); } set { _block.Variables["Version"] = value; } }

        public int NumCellsX { get { return _block.Variables["NumCellsX"].SafeParse<int>(); } set { _block.Variables["Version"] = value; } }

        public int NumCellsY { get { return _block.Variables["NumCellsY"].SafeParse<int>(); } set { _block.Variables["NumCellsY"] = value; } }

        public int OriginX { get { return _block.Variables["OriginX"].SafeParse<int>(); } set { _block.Variables["OriginX"] = value; } }

        public int OriginY { get { return _block.Variables["OriginY"].SafeParse<int>(); } set { _block.Variables["OriginY"] = value; } }

        public int OriginW { get { return _block.Variables["OriginW"].SafeParse<int>(); } set { _block.Variables["OriginW"] = value; } }

        public int OriginH { get { return _block.Variables["OriginH"].SafeParse<int>(); } set { _block.Variables["OriginH"] = value; } }

        public float TimeIndex { get { return _block.Variables["TimeIndex"].SafeParse<float>(); } set { _block.Variables["TimeIndex"] = value; } }

        public int RandomSeed { get { return _block.Variables["RandomSeed"].SafeParse<int>(); } set { _block.Variables["RandomSeed"] = value; } }

        public int SecondsPlayed { get { return _block.Variables["SecondsPlayed"].SafeParse<int>(); } set { _block.Variables["SecondsPlayed"] = value; } }

        public int ObjectId_next { get { return _block.Variables["ObjectId.next"].SafeParse<int>(); } set { _block.Variables["ObjectId.next"] = value; } }

        public bool EnabledElectricity { get { return _block.Variables["EnabledElectricity"].SafeParse<bool>(); } set { _block.Variables["EnabledElectricity"] = value; } }

        public bool EnabledWater { get { return _block.Variables["EnabledWater"].SafeParse<bool>(); } set { _block.Variables["EnabledWater"] = value; } }

        public bool EnabledFood { get { return _block.Variables["EnabledFood"].SafeParse<bool>(); } set { _block.Variables["EnabledFood"] = value; } }

        public bool EnabledMisconduct { get { return _block.Variables["EnabledMisconduct"].SafeParse<bool>(); } set { _block.Variables["EnabledMisconduct"] = value; } }

        public bool EnabledDecay { get { return _block.Variables["EnabledDecay"].SafeParse<bool>(); } set { _block.Variables["EnabledDecay"] = value; } }

        public bool EnabledVisibility { get { return _block.Variables["EnabledVisibility"].SafeParse<bool>(); } set { _block.Variables["EnabledVisibility"] = value; } }

        public bool ObjectsCentreAligned { get { return _block.Variables["ObjectsCentreAligned"].SafeParse<bool>(); } set { _block.Variables["ObjectsCentreAligned"] = value; } }

        public int BioVersions { get { return _block.Variables["BioVersions"].SafeParse<int>(); } set { _block.Variables["BioVersions"] = value; } }

        public float Intake_next { get { return _block.Variables["Intake.next"].SafeParse<float>(); } set { _block.Variables["Intake.next"] = value; } }

        public int Intake_numPrisoners { get { return _block.Variables["Intake.numPrisoners"].SafeParse<int>(); } set { _block.Variables["Intake.numPrisoners"] = value; } }

        public bool Intake_reqMin { get { return _block.Variables["Intake.reqMin"].SafeParse<bool>(); } set { _block.Variables["Intake.reqMin"] = value; } }

        public bool Intake_reqNormal { get { return _block.Variables["Intake.reqNormal"].SafeParse<bool>(); } set { _block.Variables["Intake.reqNormal"] = value; } }

        public bool Intake_reqMax { get { return _block.Variables["Intake.reqMax"].SafeParse<bool>(); } set { _block.Variables["Intake.reqMax"] = value; } }

        public IEnumerable<Cell> Cells { get { return _block.Blocks.First(block => block.BlockName == "Cells").Blocks.Select(block => new Cell(block)); } }

        public IEnumerable<ObjectBase> Objects
        {
            get
            {
                List<ObjectBase> output = new List<ObjectBase>();

                foreach (ObjectBase o in _block.Blocks.First(block => block.BlockName == "Objects").Blocks.Select(b => new ObjectBase(b)))
                {
                    ObjectBase o2 = null;

                    Type type = GetCustomType(o.Type);
                    if (type != null) o2 = Activator.CreateInstance(type, o.Block) as ObjectBase;

                    // add the custom class if we find one
                    output.Add(o2 ?? o);
                }

                return output;
            }
        }

        // ReSharper restore InconsistentNaming

        #endregion Variables

        private readonly PrisonFileParser _prisonFileParser = new PrisonFileParser();

        public PrisonFile(string filename)
        {
            FileName = filename;

            MyConsole.Name = "prisonfile";
            MyConsole.WriteLine(filename);
            MyConsole.WriteLine(new string('-', filename.Length));

            // Read the file and display it line by line.
            StreamReader file = new StreamReader(filename);

            _prisonFileParser.Parse(file.ReadToEnd(), out _block);

            file.Close();

            if (GameVersion != Version) MyConsole.WriteLine("Code was created for version " + GameVersion + " not " + Version + " problems may occur");

#if DEBUG
            // this fun little piece of code lets me see any variables I haven't wrapped yet

            List<ObjectBase> test = Objects.OrderBy(o => o.Type).ToList();
            foreach (string t in test.Select(o => o.Type).Distinct().OrderBy(type => type))
            {
                string type = t;
                Type objectType = GetCustomType(type);

                Dictionary<string, object> unhandled = new Dictionary<string, object>();
                foreach (KeyValuePair<string, object> keyValuePair in test.Where(o => o.Type == type).SelectMany(objectBase => objectBase.GetUnhandledVariables()))
                    unhandled[keyValuePair.Key] = keyValuePair.Value;
                MyConsole.WriteLine((type + " ").PadRight(20, '-') + " " + (objectType ?? typeof (ObjectBase)));
                foreach (string k in unhandled.Keys.OrderBy(key => key))
                    MyConsole.WriteLine(new string(' ', 2) + k + " : " + unhandled[k]);
            }

            MyConsole.WriteLogFile();
#endif
        }

        public string FileName { get; set; }

        public string Output { get { return _block.Output; } }

        private Type GetCustomType(string typeName)
        {
            const string className = "PrisonArchitect.PrisonFile.BlockWrappers.Objects.";
            return Type.GetType(className + typeName) ?? Type.GetType(className + "Characters." + typeName);
        }

        public void DebugBlocks()
        {
            DebugBlock(_block);
            DebugBlockVariables(_block);
#if DEBUG
            MyConsole.WriteLogFile();
#endif
        }

        private void DebugBlock(Block block)
        {
            if (block.GetType() == typeof (Block) && !string.IsNullOrEmpty(block.BlockName))
            {
                string fullName = block.BlockName;
                Block tempBlock = block.Parent;
                while (tempBlock != null)
                {
                    fullName = tempBlock.BlockName + " -> " + fullName;
                    tempBlock = tempBlock.Parent;
                }
                MyConsole.WriteLine("Unhandled Block: " + fullName);
            }

            foreach (Block b in block.Blocks)
                DebugBlock(b);
        }

        private void DebugBlockVariables(Block block)
        {
            MyConsole.Write(block.ToString());
            foreach (Block b in block.Blocks)
                DebugBlockVariables(b);
        }
    }
}