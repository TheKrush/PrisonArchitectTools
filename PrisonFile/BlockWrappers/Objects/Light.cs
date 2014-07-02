using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.BlockWrappers.Base;

namespace PrisonArchitect.PrisonFile.BlockWrappers.Objects
{
    public class Light : Electrical
    {
        public Light(Block block) : base(block) { }

        #region Variables

        // ReSharper disable InconsistentNaming

        public bool ExternalPower { get { return Variables["ExternalPower"].SafeParse<bool>(); } set { Variables["ExternalPower"] = value; } }

        // ReSharper restore InconsistentNaming

        #endregion Variables
    }
}
