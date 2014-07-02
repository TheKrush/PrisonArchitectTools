using PrisonArchitect.PrisonFile.BlockWrappers.Helper;

namespace PrisonArchitect.PrisonFile.BlockWrappers.Objects
{
    public class VisitorTable : ObjectBase
    {
        public VisitorTable(Block block) : base(block) { Visitor = new Id(block, "Visitor"); }

        public Id Visitor { get; set; }
    }
}