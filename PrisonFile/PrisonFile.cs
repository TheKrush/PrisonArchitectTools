using System.IO;
using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.Blocks;

namespace PrisonArchitect.PrisonFile
{
    public class PrisonFile
    {
        public static string GameVersion = "alpha-22";

        #region Variables

        public string Version
        {
            get { return _block.Variables["Version"].SafeParse<string>(); }
            set { _block.Variables["Version"] = value; }
        }

        public int NumCellsX
        {
            get { return _block.Variables["NumCellsX"].SafeParse<int>(); }
            set { _block.Variables["Version"] = value; }
        }

        public int NumCellsY
        {
            get { return _block.Variables["NumCellsY"].SafeParse<int>(); }
            set { _block.Variables["NumCellsY"] = value; }
        }

        public int OriginX
        {
            get { return _block.Variables["OriginX"].SafeParse<int>(); }
            set { _block.Variables["OriginX"] = value; }
        }

        public int OriginY
        {
            get { return _block.Variables["OriginY"].SafeParse<int>(); }
            set { _block.Variables["OriginY"] = value; }
        }

        public int OriginW
        {
            get { return _block.Variables["OriginW"].SafeParse<int>(); }
            set { _block.Variables["OriginW"] = value; }
        }

        public int OriginH
        {
            get { return _block.Variables["OriginH"].SafeParse<int>(); }
            set { _block.Variables["OriginH"] = value; }
        }

        public float TimeIndex
        {
            get { return _block.Variables["TimeIndex"].SafeParse<float>(); }
            set { _block.Variables["TimeIndex"] = value; }
        }

        public int RandomSeed
        {
            get { return _block.Variables["RandomSeed"].SafeParse<int>(); }
            set { _block.Variables["RandomSeed"] = value; }
        }

        public int SecondsPlayed
        {
            get { return _block.Variables["SecondsPlayed"].SafeParse<int>(); }
            set { _block.Variables["SecondsPlayed"] = value; }
        }

// ReSharper disable InconsistentNaming
        public int ObjectId_next
// ReSharper restore InconsistentNaming
        {
            get { return _block.Variables["ObjectId.next"].SafeParse<int>(); }
            set { _block.Variables["ObjectId.next"] = value; }
        }

        public bool EnabledElectricity
        {
            get { return _block.Variables["EnabledElectricity"].SafeParse<bool>(); }
            set { _block.Variables["EnabledElectricity"] = value; }
        }

        public bool EnabledWater
        {
            get { return _block.Variables["EnabledWater"].SafeParse<bool>(); }
            set { _block.Variables["EnabledWater"] = value; }
        }

        public bool EnabledFood
        {
            get { return _block.Variables["EnabledFood"].SafeParse<bool>(); }
            set { _block.Variables["EnabledFood"] = value; }
        }

        public bool EnabledMisconduct
        {
            get { return _block.Variables["EnabledMisconduct"].SafeParse<bool>(); }
            set { _block.Variables["EnabledMisconduct"] = value; }
        }

        public bool EnabledDecay
        {
            get { return _block.Variables["EnabledDecay"].SafeParse<bool>(); }
            set { _block.Variables["EnabledDecay"] = value; }
        }

        public bool EnabledVisibility
        {
            get { return _block.Variables["EnabledVisibility"].SafeParse<bool>(); }
            set { _block.Variables["EnabledVisibility"] = value; }
        }

        public bool ObjectsCentreAligned
        {
            get { return _block.Variables["ObjectsCentreAligned"].SafeParse<bool>(); }
            set { _block.Variables["ObjectsCentreAligned"] = value; }
        }

        public int BioVersions
        {
            get { return _block.Variables["BioVersions"].SafeParse<int>(); }
            set { _block.Variables["BioVersions"] = value; }
        }

// ReSharper disable InconsistentNaming
        public float Intake_next
// ReSharper restore InconsistentNaming
        {
            get { return _block.Variables["Intake.next"].SafeParse<float>(); }
            set { _block.Variables["Intake.next"] = value; }
        }

// ReSharper disable InconsistentNaming
        public int Intake_numPrisoners
// ReSharper restore InconsistentNaming
        {
            get { return _block.Variables["Intake.numPrisoners"].SafeParse<int>(); }
            set { _block.Variables["Intake.numPrisoners"] = value; }
        }

// ReSharper disable InconsistentNaming
        public bool Intake_reqMin
// ReSharper restore InconsistentNaming
        {
            get { return _block.Variables["Intake.reqMin"].SafeParse<bool>(); }
            set { _block.Variables["Intake.reqMin"] = value; }
        }

// ReSharper disable InconsistentNaming
        public bool Intake_reqNormal
// ReSharper restore InconsistentNaming
        {
            get { return _block.Variables["Intake.reqNormal"].SafeParse<bool>(); }
            set { _block.Variables["Intake.reqNormal"] = value; }
        }

// ReSharper disable InconsistentNaming
        public bool Intake_reqMax
// ReSharper restore InconsistentNaming
        {
            get { return _block.Variables["Intake.reqMax"].SafeParse<bool>(); }
            set { _block.Variables["Intake.reqMax"] = value; }
        }

        public Cells Cells
        {
            get { return _block.Blocks.Find(block => block.BlockName == "Cells") as Cells; }
        }

        public Objects Objects
        {
            get { return _block.Blocks.Find(block => block.BlockName == "Objects") as Objects; }
        }

        #endregion Variables

        private readonly Block _block;
        private readonly PrisonFileParser _prisonFileParser = new PrisonFileParser();

        public PrisonFile(string filename)
        {
            FileName = filename;

            MyConsole.WriteLine(filename);

            // Read the file and display it line by line.
            StreamReader file = new StreamReader(filename);

            _prisonFileParser.Parse(file.ReadToEnd(), out _block);

            file.Close();

            if (GameVersion != Version)
            {
                MyConsole.WriteLine("Code was created for version " + GameVersion + " not " + Version +
                                    " problems may occur");
            }

#if DEBUG
            string s = Output;
            MyConsole.WriteLogFile();
#endif
        }

        public string FileName { get; set; }

        public string Output
        {
            get { return _block.Output; }
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