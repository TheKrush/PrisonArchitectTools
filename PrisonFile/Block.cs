using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PrisonArchitect.Helper;

namespace PrisonArchitect.PrisonFile
{
    public class Block
    {
        public string BlockName = "";
        public List<Block> Blocks = new List<Block>();
        public int CurrentDepth = 0;

        public Block Parent = null;
        public string RawData = "";
        public SafeDictionary<string, object> Variables = new SafeDictionary<string, object>();

        public Block() { }

        public Block(Block b)
        {
            Blocks = b.Blocks;
            CurrentDepth = b.CurrentDepth;
            BlockName = b.BlockName;
            Parent = b.Parent;
            RawData = b.RawData;
            Variables = b.Variables;
        }

        public string BlockPath
        {
            get
            {
                string blockName = (BlockName.Contains("\"") ? GetType().Name : BlockName);
                if (Parent != null && !string.IsNullOrEmpty(Parent.BlockName))
                    return Parent.BlockPath + "." + blockName;
                return blockName;
            }
        }

        public string Output
        {
            get
            {
                string s = "";

                if (BlockName == "\"[i 97]\"")
                    MyConsole.WriteLine(BlockName);

                if (!string.IsNullOrEmpty(BlockName))
                    s += new String(' ', CurrentDepth) + "BEGIN " + BlockName + Environment.NewLine;

                foreach (KeyValuePair<string, object> keyValuePair in Variables)
                {
                    foreach (string value in keyValuePair.Value.ToString().Split(','))
                    {
                        int extraIndent = 0;
                        if (!string.IsNullOrEmpty(BlockName)) extraIndent = 1;
                        s += new String(' ', CurrentDepth + extraIndent) + keyValuePair.Key + " " + value +
                             Environment.NewLine;
                    }
                }

                s = Blocks.Aggregate(s, (current, block) => current + block.Output);

                if (!string.IsNullOrEmpty(BlockName))
                    s += new String(' ', CurrentDepth) + "END" + Environment.NewLine;

                return s;
            }
        }

        public void ParseVariables()
        {
            // remove all the sub-b raw data so we don't parse it
            string currentBlockRawData = Blocks.Aggregate(RawData,
                                                          (current, block) => current.Replace(block.RawData, ""));

            foreach (
                string line in
                    currentBlockRawData.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
            {
                string formattedLine = Regex.Replace(line.Trim(), @"\s+", " ").Trim();

                // remove our BEGIN, BlockName, and END
                if (formattedLine.StartsWith("BEGIN " + BlockName))
                    formattedLine = formattedLine.Remove(0, ("BEGIN " + BlockName).Length).Trim();
                if (formattedLine.EndsWith("END"))
                    formattedLine = formattedLine.Substring(0,
                                                            formattedLine.LastIndexOf("END", StringComparison.Ordinal));

                // temporarily replace these spaces with underscores
                foreach (Match match in Regex.Matches(formattedLine, "\"([^\"]*)\""))
                {
                    string formattedWord = match.ToString().Replace(" ", "_");
                    formattedLine = formattedLine.Replace(match.ToString(), formattedWord);
                }

                // split our string into key / value pairs
                List<string> pairs = formattedLine.Split(' ')
                    .Select((s, i) => new {s, i})
                    .GroupBy(n => n.i/2)
                    .Select(g => string.Join(" ", g.Select(p => p.s).ToArray()))
                    .Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                foreach (string s in pairs)
                {
                    string key = s.Split(null)[0];
                    string value = s.Split(null)[1];

                    if (Regex.IsMatch(key, "\"([^\"]*)\"")) key = key.Replace("_", " ");
                    if (Regex.IsMatch(value, "\"([^\"]*)\"")) value = value.Replace("_", " ");

                    // some variables seem to appear more than once so we need to handle that
                    if (Variables.ContainsKey(key)) Variables[key] = Variables[key] + "," + value;
                    else Variables.Add(key, value);
                }
            }
        }

        public void Print(bool prisonFormat = false)
        {
            if (prisonFormat)
            {
                if (!string.IsNullOrEmpty(BlockName))
                    MyConsole.WriteLine(new String(' ', CurrentDepth) + "BEGIN " + BlockName);
            }
            else
                MyConsole.WriteLine(new String(' ', (CurrentDepth*2)) + "- " + BlockName);
            PrintVariables(prisonFormat);
            foreach (Block block in Blocks)
                block.Print(prisonFormat);
            if (prisonFormat)
                MyConsole.WriteLine(new String(' ', CurrentDepth) + "END");
        }

        private void PrintVariables(bool prisonFormat = false)
        {
            foreach (KeyValuePair<string, object> keyValuePair in Variables)
            {
                foreach (string value in keyValuePair.Value.ToString().Split(','))
                {
                    if (prisonFormat)
                    {
                        int extraIndent = 0;
                        if (!string.IsNullOrEmpty(BlockName)) extraIndent = 1;
                        MyConsole.WriteLine(new String(' ', CurrentDepth + extraIndent) + keyValuePair.Key + " " + value);
                    }
                    else
                        MyConsole.WriteLine(new String(' ', (CurrentDepth + 1)*2) + keyValuePair.Key + " = " + value);
                }
            }
        }

        public override string ToString()
        {
            string s = BlockName + " (" + GetType() + ")" + Environment.NewLine;
            s = Variables.Aggregate(s, (current, keyValuePair) => current + (new string(' ', 2) + keyValuePair.Key + " : " + keyValuePair.Value + Environment.NewLine));
            s += new string(' ', 2) + "Blocks : " + Blocks.Count + Environment.NewLine;
            return s;
        }
    }
}