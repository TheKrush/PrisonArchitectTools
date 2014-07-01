using System;
using System.Reflection;
using System.Text.RegularExpressions;
using PrisonArchitect.PrisonFile.Blocks;

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

                    string className = "";
                    if (string.IsNullOrEmpty(_currentBlock.BlockName))
                        className = "PrisonArchitect.PrisonFile.Blocks." + name;
                    else
                        className = _currentBlock.GetType().FullName + "+" + name;

                    Block newBlock = Assembly.GetExecutingAssembly().CreateInstance(className) as Block;
                    if (newBlock != null)
                    {
                        newBlock.BlockName = name;
                        newBlock.CurrentDepth = _currentBlock.CurrentDepth + 1;
                        newBlock.Parent = _currentBlock;
                        _currentBlock = newBlock;
                    }
                    else
                    {
                        switch (_currentBlock.BlockPath)
                        {
                            case "Cells":
                                _currentBlock = new Cells.Cell
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Contraband.HistoricalTrackers":
                                _currentBlock = new Contraband.HistoricalTrackers.HistoricalTracker
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Contraband.HistoricalTrackers.HistoricalTracker.Log":
                                _currentBlock = new Contraband.HistoricalTrackers.HistoricalTracker.Log.SubLog
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Contraband.Trackers":
                                _currentBlock = new Contraband.Trackers.Tracker
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Contraband.Trackers.Tracker.Log":
                                _currentBlock = new Contraband.Trackers.Tracker.Log.SubLog
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Contraband.Prisoners":
                                _currentBlock = new Contraband.Prisoners.Prisoner
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Electricity":
                                _currentBlock = new Electricity.Cell
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Misconduct.MisconductReports":
                                _currentBlock = new Misconduct.MisconductReports.MisconductReport
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Misconduct.MisconductReports.MisconductReport.MisconductEntries":
                                _currentBlock = new Misconduct.MisconductReports.MisconductReport.MisconductEntries.
                                    MisconductEntry
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Objects":
                                _currentBlock = new Objects.Object
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Patrols":
                                _currentBlock = new Patrols.Cell
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Penalties":
                                _currentBlock = new Penalties.SubPenalties
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Penalties.Penalties":
                                _currentBlock = new Penalties.SubPenalties.Item
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Reform.Programs":
                                _currentBlock = new Reform.Programs.Program
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Reform.Programs.Program.Objects":
                                _currentBlock = new Reform.Programs.Program.Objects.Object
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Sectors":
                                _currentBlock = new Sectors.SubSectors
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Sectors.Sectors":
                                _currentBlock = new Sectors.SubSectors.Sector
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Sectors.Sectors.Sector.Jobs":
                                _currentBlock = new Sectors.SubSectors.Sector.Jobs.Job
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Sectors.Sectors.Sector.Stations":
                                _currentBlock = new Sectors.SubSectors.Sector.Stations.Station
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Squads":
                                _currentBlock = new Squads.SubSquads
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Rooms":
                                _currentBlock = new Rooms.Room
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Victory.Log":
                                _currentBlock = new Victory.Log.SubLog
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Visibility":
                                _currentBlock = new Visibility.Cell
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Visitation":
                                _currentBlock = new Visitation.SubVisitation
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "Water":
                                _currentBlock = new Water.Cell
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            case "WorkQ.Items":
                                _currentBlock = new WorkQ.Items.Item
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                            default:
                                _currentBlock = new Block
                                                    {
                                                        BlockName = name,
                                                        CurrentDepth = _currentBlock.CurrentDepth + 1,
                                                        Parent = _currentBlock
                                                    };
                                break;
                        }
                    }

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