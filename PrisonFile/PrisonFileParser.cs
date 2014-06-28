using System;
using System.Text.RegularExpressions;

namespace PrisonArchitect.PrisonFile
{
    public class PrisonFileParser
    {
        private readonly Block _block = new Block {CurrentDepth = 0};
        private Block _currentBlock;

        public void Parse(string input, out Block block)
        {
            _currentBlock = _block;

            foreach (string line in input.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
            {
                string formattedLine = line.Trim();
                if (String.IsNullOrEmpty(formattedLine)) continue;

                formattedLine = Regex.Replace(formattedLine, @"\s+", " ");

                if (formattedLine.StartsWith("BEGIN"))
                {
                    // get this new blocks name
                    string name = Regex.Match(formattedLine, "\"([^\"]*)\"").ToString();
                    if (String.IsNullOrEmpty(name)) name = formattedLine.Split(null)[1];
                    name = name.Trim();

                    _currentBlock = new Block
                                        {
                                            Name = name,
                                            CurrentDepth = _currentBlock.CurrentDepth + 1,
                                            Parent = _currentBlock
                                        };
                    _currentBlock.Parent.Blocks.Add(_currentBlock);
                }

                _currentBlock.RawData += line + Environment.NewLine;

                if (formattedLine.EndsWith("END"))
                {
                    _currentBlock.ParseVariables();
                    _currentBlock = _currentBlock.Parent;
                }
            }

            _block.ParseVariables();
            block = _block;
        }
    }
}