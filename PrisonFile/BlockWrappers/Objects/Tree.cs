using PrisonArchitect.Helper;

namespace PrisonArchitect.PrisonFile.BlockWrappers.Objects
{
    public class Tree : ObjectBase
    {
        public Tree(Block block) : base(block) { }

        #region Variables

        // ReSharper disable InconsistentNaming

        public float Age { get { return Variables["Age"].SafeParse<float>(); } set { Variables["Age"] = value; } }

        // ReSharper restore InconsistentNaming

        #endregion Variables
    }
}
