using PrisonArchitect.Helper;

namespace PrisonArchitect.PrisonFile.BlockWrappers.Base
{
    public class JobTool : ObjectBase
    {
        public JobTool(Block block) : base(block) { }

        #region Variables

        // ReSharper disable InconsistentNaming

        public int JobId { get { return Variables["JobId"].SafeParse<int>(); } set { Variables["JobId"] = value; } }

        // ReSharper restore InconsistentNaming

        #endregion Variables
    }
}
