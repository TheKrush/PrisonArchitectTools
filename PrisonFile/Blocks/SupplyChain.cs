using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class SupplyChain : HandledBlock
    {
        #region Variables

        public float Timer
        {
            get { return Variables["Timer"].SafeParse<float>(); }
            set { Variables["Timer"] = value; }
        }

        public int PreviousHour
        {
            get { return Variables["PreviousHour"].SafeParse<int>(); }
            set { Variables["PreviousHour"] = value; }
        }

        public int PreviousActualHour
        {
            get { return Variables["PreviousActualHour"].SafeParse<int>(); }
            set { Variables["PreviousActualHour"] = value; }
        }

        #endregion Variables

        #region Nested type: Order

        public class Order : HandledBlock
        {
        }

        #endregion
    }
}