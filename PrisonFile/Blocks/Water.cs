using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Water : HandledBlock
    {
        #region Nested type: Cell

        public class Cell : HelperBlocks.Cell
        {
            #region Variables

            public int PipeType
            {
                get { return Variables["PipeType"].SafeParse<int>(); }
                set { Variables["PipeType"] = value; }
            }

            public float PressureX
            {
                get { return Variables["PressureX"].SafeParse<float>(); }
                set { Variables["PressureX"] = value; }
            }

            public float PressureY
            {
                get { return Variables["PressureY"].SafeParse<float>(); }
                set { Variables["PressureY"] = value; }
            }

            #endregion Variables
        }

        #endregion
    }
}