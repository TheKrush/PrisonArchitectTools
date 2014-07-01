using System;
using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Cells : HandledBlock
    {
        public Cell GetCell(string key)
        {
            return Blocks.Find(block => block.BlockName == key) as Cell;
        }

        #region Nested type: Cell

        public class Cell : HelperBlocks.Cell
        {
            #region Variables

            public string Mat
            {
                get
                {
                    try
                    {
                        return Variables["Mat"].SafeParse<string>();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                set { Variables["Mat"] = value; }
            }

            public string Material
            {
                get { return Mat; }
                set { Mat = value; }
            }

            public float Con
            {
                get { return Variables["Con"].SafeParse<float>(); }
                set { Variables["Con"] = value; }
            }

            public float Condition
            {
                get { return Con; }
                set { Con = value; }
            }

// ReSharper disable InconsistentNaming
            public int Room_i
// ReSharper restore InconsistentNaming
            {
                get { return Variables["Room.i"].SafeParse<int>(); }
                set { Variables["Room.i"] = value; }
            }


// ReSharper disable InconsistentNaming
            public int Room_u
// ReSharper restore InconsistentNaming
            {
                get { return Variables["Room.u"].SafeParse<int>(); }
                set { Variables["Room.u"] = value; }
            }

            public bool Ind
            {
                get { return Variables["Ind"].SafeParse<bool>(); }
                set { Variables["Ind"] = value; }
            }

            public bool Indoors
            {
                get { return Ind; }
                set { Ind = value; }
            }

            #endregion Variables
        }

        #endregion
    }
}