using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PrisonArchitect.Helper;

namespace PrisonArchitect.PrisonFile
{
    public class Block
    {
        public List<Block> Blocks = new List<Block>();
        public int CurrentDepth = 0;
        public bool Handled = false;
        public string Name = "";
        public Block Parent = null;
        public string RawData = "";
        public Dictionary<string, object> Variables = new Dictionary<string, object>();

        public void ParseVariables()
        {
            // remove all the sub-block raw data so we don't parse it
            string currentBlockRawData = Blocks.Aggregate(RawData,
                                                          (current, block) => current.Replace(block.RawData, ""));

            foreach (
                string line in
                    currentBlockRawData.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
            {
                string formattedLine = Regex.Replace(line.Trim(), @"\s+", " ").Trim();

                // remove our BEGIN, Name, and END
                if (formattedLine.StartsWith("BEGIN " + Name))
                    formattedLine = formattedLine.Remove(0, ("BEGIN " + Name).Length).Trim();
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

        public void Print(bool logFormat = false)
        {
            if (logFormat)
            {
                if (!string.IsNullOrEmpty(Name))
                    MyConsole.WriteLine(new String(' ', CurrentDepth) + "BEGIN " + Name);
            }
            else
                MyConsole.WriteLine(new String(' ', (CurrentDepth*2)) + "- " + Name);
            PrintVariables(logFormat);
            foreach (Block block in Blocks)
                block.Print(logFormat);
            if (logFormat)
                MyConsole.WriteLine(new String(' ', CurrentDepth) + "END");
        }

        private void PrintVariables(bool logFormat = false)
        {
            foreach (KeyValuePair<string, object> keyValuePair in Variables)
            {
                if (logFormat)
                {
                    int extraIndent = 0;
                    if (!string.IsNullOrEmpty(Name))
                        extraIndent = 1;
                    MyConsole.WriteLine(new String(' ', CurrentDepth + extraIndent) + keyValuePair.Key + " " +
                                        keyValuePair.Value);
                }
                else
                    MyConsole.WriteLine(new String(' ', (CurrentDepth + 1)*2) + keyValuePair.Key + " = " +
                                        keyValuePair.Value);
            }
        }

        public override string ToString()
        {
            string s;

            // if we haven't handled this block yet just spit out the raw data
            if (!Handled)
            {
                // need to remove our END and put it after our children's END
                s = RawData.Substring(0, RawData.LastIndexOf("END", StringComparison.Ordinal));
                s = Blocks.Aggregate(s, (current, block) => current + block);
                s += "END";
                return s;
            }

            s = Variables.Aggregate("",
                                    (current, keyValuePair) =>
                                    current + (keyValuePair.Key + " " + keyValuePair.Value + Environment.NewLine));
            return Blocks.Aggregate(s, (current, b) => current + b);
        }
    }
}