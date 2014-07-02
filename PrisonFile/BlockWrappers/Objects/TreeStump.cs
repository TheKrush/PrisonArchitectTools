using PrisonArchitect.Helper;

namespace PrisonArchitect.PrisonFile.BlockWrappers.Objects
{
    public class TreeStump : ObjectBase
    {
        public TreeStump(Block block) : base(block) { }

        #region Variables

        // ReSharper disable InconsistentNaming

        public float Lifetime { get { return Variables["Lifetime"].SafeParse<float>(); } set { Variables["Lifetime"] = value; } }

        // ReSharper restore InconsistentNaming

        #endregion Variables
    }
}