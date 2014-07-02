using PrisonArchitect.Helper;

namespace PrisonArchitect.PrisonFile.BlockWrappers.Base
{
    public class Electrical : ObjectBase
    {
        public Electrical(Block block) : base(block) { }

        #region Variables

        // ReSharper disable InconsistentNaming

        public bool Powered { get { return Variables["Powered"].SafeParse<bool>(); } set { Variables["Powered"] = value; } }

        public bool On { get { return Variables["On"].SafeParse<bool>(); } set { Variables["On"] = value; } }

        // ReSharper restore InconsistentNaming

        #endregion Variables
    }
}
