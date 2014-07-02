namespace PrisonArchitect.PrisonFile.BlockWrappers.Helper
{
    public class HelperBase : BlockWrapper
    {
        protected string Name;

        public HelperBase(Block block, string name) : base(block) { Name = name; }
    }
}