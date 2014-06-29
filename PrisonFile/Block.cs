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
        public bool Handled = false;

        public Block Parent = null;
        public string RawData = "";
        public Dictionary<string, object> Variables = new Dictionary<string, object>();

        public Block()
        {
        }

        public Block(Block b)
        {
            Blocks = b.Blocks;
            CurrentDepth = b.CurrentDepth;
            Handled = b.Handled;
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

        public List<string> FindUnusedVariables()
        {
            return
                Variables.Keys.Where(
                    key => !GetType().GetProperties().Select(prop => prop.Name).ToList().Contains(key.Replace(".", "_")))
                    .ToList();
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

                    if (Variables.ContainsKey(key)) Variables[key] = value;
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
                if (prisonFormat)
                {
                    int extraIndent = 0;
                    if (!string.IsNullOrEmpty(BlockName))
                        extraIndent = 1;
                    MyConsole.WriteLine(new String(' ', CurrentDepth + extraIndent) + keyValuePair.Key + " " +
                                        keyValuePair.Value);
                }
                else
                    MyConsole.WriteLine(new String(' ', (CurrentDepth + 1)*2) + keyValuePair.Key + " = " +
                                        keyValuePair.Value);
            }
        }

        public string Output()
        {
            string s = "";

            // if we haven't handled this b yet just spit out the raw data
            if (!Handled)
            {
                // need to remove our END and put it after our children's END
                s += RawData.Substring(0, RawData.LastIndexOf("END", StringComparison.Ordinal));
                s += Blocks.Aggregate(s, (current, block) => current + block);
                s += "END";
            }
            else
            {
                if (string.IsNullOrEmpty(BlockName))
                    s += new string(' ', CurrentDepth*2) + "BEGIN " + BlockName + Environment.NewLine;
                s += Variables.Aggregate(s,
                                         (current, keyValuePair) =>
                                         current +
                                         (new string(' ', (CurrentDepth + 1)*2) + keyValuePair.Key + " " +
                                          keyValuePair.Value + Environment.NewLine));
                s += Blocks.Aggregate(s, (current, block) => current + block);
                if (string.IsNullOrEmpty(BlockName))
                    s += new string(' ', CurrentDepth*2) + "END" + Environment.NewLine;
            }

            return s;
        }

        public override string ToString()
        {
            string s = "";

#if DEBUG
            List<string> unusedVariables = FindUnusedVariables();
            if (Handled && unusedVariables.Count > 0)
            {
#endif
                s += GetType().FullName + Environment.NewLine;
                s += new string(' ', 2) + "BlockName : " + BlockName + Environment.NewLine;
                s = Variables.Aggregate(s,
                                        (current, keyValuePair) =>
                                        current +
                                        (new string(' ', 2) + keyValuePair.Key + " : " + keyValuePair.Value +
                                         Environment.NewLine));
#if DEBUG
                s += new string(' ', 2) + new string('-', 10) + Environment.NewLine;
                s = FindUnusedVariables().Aggregate(s,
                                                    (current, key) =>
                                                    current +
                                                    (new string(' ', 2) + key + " : " + Variables[key] +
                                                     Environment.NewLine));
            }
#endif
            return s;
        }
    }
}