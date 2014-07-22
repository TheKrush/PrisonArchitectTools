using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.BlockWrappers;
using PrisonArchitect.PrisonFile.BlockWrappers.Base;
using PrisonArchitect.PrisonFile.BlockWrappers.Helper;
using PrisonArchitect.PrisonFile.BlockWrappers.Objects;

namespace PrisonArchitect.PrisonFile
{
    public class PrisonFile : BlockWrapper
    {
        public const string GameVersion = "alpha-22";

        #region Variables

        // ReSharper disable InconsistentNaming

        public int BioVersions { get { return Variables["BioVersions"].SafeParse<int>(); } set { Variables["BioVersions"] = value; } }

        public bool CeoLetterRead { get { return Variables["CeoLetterRead"].SafeParse<bool>(); } set { Variables["CeoLetterRead"] = value; } }

        public bool EnabledDecay { get { return Variables["EnabledDecay"].SafeParse<bool>(); } set { Variables["EnabledDecay"] = value; } }

        public bool EnabledElectricity { get { return Variables["EnabledElectricity"].SafeParse<bool>(); } set { Variables["EnabledElectricity"] = value; } }

        public bool EnabledFood { get { return Variables["EnabledFood"].SafeParse<bool>(); } set { Variables["EnabledFood"] = value; } }

        public bool EnabledMisconduct { get { return Variables["EnabledMisconduct"].SafeParse<bool>(); } set { Variables["EnabledMisconduct"] = value; } }

        public bool EnabledVisibility { get { return Variables["EnabledVisibility"].SafeParse<bool>(); } set { Variables["EnabledVisibility"] = value; } }

        public bool EnabledWater { get { return Variables["EnabledWater"].SafeParse<bool>(); } set { Variables["EnabledWater"] = value; } }

        public bool FailureConditions { get { return Variables["FailureConditions"].SafeParse<bool>(); } set { Variables["FailureConditions"] = value; } }

        public int FoodQuantity { get { return Variables["FoodQuantity"].SafeParse<int>(); } set { Variables["FoodQuantity"] = value; } }

        public int FoodVariation { get { return Variables["FoodVariation"].SafeParse<int>(); } set { Variables["FoodVariation"] = value; } }

        public bool GenerateForests { get { return Variables["GenerateForests"].SafeParse<bool>(); } set { Variables["GenerateForests"] = value; } }

        public int NumCellsX { get { return Variables["NumCellsX"].SafeParse<int>(); } set { Variables["NumCellsX"] = value; } }

        public int NumCellsY { get { return Variables["NumCellsY"].SafeParse<int>(); } set { Variables["NumCellsY"] = value; } }

        public bool ObjectsCentreAligned { get { return Variables["ObjectsCentreAligned"].SafeParse<bool>(); } set { Variables["ObjectsCentreAligned"] = value; } }

        public int OriginH { get { return Variables["OriginH"].SafeParse<int>(); } set { Variables["OriginH"] = value; } }

        public int OriginW { get { return Variables["OriginW"].SafeParse<int>(); } set { Variables["OriginW"] = value; } }

        public int OriginX { get { return Variables["OriginX"].SafeParse<int>(); } set { Variables["OriginX"] = value; } }

        public int OriginY { get { return Variables["OriginY"].SafeParse<int>(); } set { Variables["OriginY"] = value; } }

        public int RandomSeed { get { return Variables["RandomSeed"].SafeParse<int>(); } set { Variables["RandomSeed"] = value; } }

        public int SecondsPlayed { get { return Variables["SecondsPlayed"].SafeParse<int>(); } set { Variables["SecondsPlayed"] = value; } }

        public float TimeIndex { get { return Variables["TimeIndex"].SafeParse<float>(); } set { Variables["TimeIndex"] = value; } }

        public float TimeWarpFactor { get { return Variables["TimeWarpFactor"].SafeParse<float>(); } set { Variables["TimeWarpFactor"] = value; } }

        public string Version { get { return Variables["Version"].SafeParse<string>(); } set { Variables["Version"] = value; } }

        public IEnumerable<Cell> Cells { get { return Blocks.First(block => block.BlockName == "Cells").Blocks.Select(block => new Cell(block)); } }

        public Finance Finance { get { return new Finance(Blocks.First(block => block.BlockName == "Finance")); } }

        public Intake Intake { get; set; }

        public Id ObjectId { get; set; }

        public IEnumerable<ObjectBase> Objects
        {
            get
            {
                List<ObjectBase> output = new List<ObjectBase>();

                foreach (ObjectBase o in Blocks.First(block => block.BlockName == "Objects").Blocks.Select(b => new ObjectBase(b)))
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

        public PrisonFile(string filename) : base(null)
        {
            FileName = filename;

            MyConsole.Name = "prisonfile";
            MyConsole.WriteLine(filename);
            MyConsole.WriteLine(new string('-', filename.Length));

            #region Parse

            // Read the file and display it line by line.
            StreamReader file = new StreamReader(filename);

            _prisonFileParser.Parse(file.ReadToEnd(), out Block);

            file.Close();

            #endregion Parse

            if (GameVersion != Version) MyConsole.WriteLine("Code was created for version " + GameVersion + " not " + Version + " problems may occur");

            Intake = new Intake(Block, "Intake");
            ObjectId = new Id(Block, "ObjectId");

#if DEBUG
            Dictionary<string, object> unhandled = new Dictionary<string, object>();

            foreach (Block block in Blocks.OrderBy(block => block.BlockName))
                MyConsole.WriteLine(block.BlockName);

            MyConsole.WriteLine(new string('-', filename.Length));

            {
                MyConsole.WriteLine("Debugging - PrisonFile");
                unhandled = new Dictionary<string, object>();
                foreach (KeyValuePair<string, object> keyValuePair in GetUnhandledVariables())
                    unhandled[keyValuePair.Key] = keyValuePair.Value;
                MyConsole.WriteLine("PrisonFile ".PadRight(20, '-') + " " + GetType().FullName);
                foreach (string k in unhandled.Keys.OrderBy(key => key))
                    MyConsole.WriteLine(new string(' ', 2) + k + " : " + unhandled[k]);
            }

            #region Objects

            { // this fun little piece of code lets me see any object variables I haven't wrapped yet
                MyConsole.WriteLine("Debugging - Objects");
                MyConsole.WriteLine(new string('-', filename.Length));
                List<ObjectBase> test = Objects.OrderBy(o => o.Type).ToList();
                foreach (string t in test.Select(o => o.Type).Distinct().OrderBy(type => type))
                {
                    string type = t;
                    Type objectType = GetCustomType(type);

                    unhandled = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, object> keyValuePair in test.Where(o => o.Type == type).SelectMany(objectBase => objectBase.GetUnhandledVariables()))
                        unhandled[keyValuePair.Key] = keyValuePair.Value;
                    MyConsole.WriteLine((type + " ").PadRight(20, '-') + " " + (objectType ?? typeof (ObjectBase)));
                    foreach (string k in unhandled.Keys.OrderBy(key => key))
                        MyConsole.WriteLine(new string(' ', 2) + k + " : " + unhandled[k]);
                }

                // Character.Bio check
                unhandled = new Dictionary<string, object>();
                foreach (
                    KeyValuePair<string, object> keyValuePair in Objects.Where(o => o.GetType() == typeof (Character)).Cast<Character>().SelectMany(character => character.Bio.GetUnhandledVariables()))
                    unhandled[keyValuePair.Key] = keyValuePair.Value;
                MyConsole.WriteLine("Bio ".PadRight(20, '-') + " " + typeof (Bio).FullName);
                foreach (string k in unhandled.Keys.OrderBy(key => key))
                    MyConsole.WriteLine(new string(' ', 2) + k + " : " + unhandled[k]);
            }

            #endregion Objects

            MyConsole.WriteLogFile();
#endif
        }

        public string FileName { get; set; }

        public string Output { get { return Block.Output; } }

        private Type GetCustomType(string typeName)
        {
            const string className = "PrisonArchitect.PrisonFile.BlockWrappers.Objects.";
            return Type.GetType(className + typeName) ?? Type.GetType(className + "Characters." + typeName);
        }

        public IEnumerable<Character> Characters { get { return Objects.OfType<Character>(); } }
        public IEnumerable<Prisoner> Prisoners { get { return Objects.OfType<Prisoner>(); } } 
    }
}
