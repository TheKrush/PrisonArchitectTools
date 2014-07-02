using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.BlockWrappers.Base;

namespace PrisonArchitect.PrisonFile.BlockWrappers.Objects
{
    public class MetalDetector : Electrical
    {
        public MetalDetector(Block block) : base(block) { }

        #region Variables

        // ReSharper disable InconsistentNaming

        public float Alarm { get { return Variables["Alarm"].SafeParse<float>(); } set { Variables["Alarm"] = value; } }

        // ReSharper restore InconsistentNaming

        #endregion Variables
    }
}