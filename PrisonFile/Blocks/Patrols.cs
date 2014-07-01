using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Patrols : HandledBlock
    {
        #region Nested type: Cell

        public class Cell : HelperBlocks.Cell
        {
            #region Variables

            public bool Set
            {
                get { return Variables["Set"].SafeParse<bool>(); }
                set { Variables["Set"] = value; }
            }

            #endregion Variables
        }

        #endregion

        #region Nested type: Stations

        public class Stations : HandledBlock
        {
        }

        #endregion
    }
}