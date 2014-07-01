using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Visibility : HandledBlock
    {
        public Cell GetCell(string key)
        {
            return Blocks.Find(block => block.BlockName == key) as Cell;
        }

        #region Nested type: Cell

        public class Cell : HelperBlocks.Cell
        {
            #region Variables

            public float Vis
            {
                get { return Variables["Vis"].SafeParse<float>(); }
                set { Variables["Vis"] = value; }
            }

            public float Visibility
            {
                get { return Vis; }
                set { Vis = value; }
            }

            #endregion Variables
        }

        #endregion
    }
}