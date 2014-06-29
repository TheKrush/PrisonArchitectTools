using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Objects : BlockList
    {
        #region Nested type: Object

        public class Object : Id
        {
            #region Variables

// ReSharper disable InconsistentNaming
            public float Pos_x
// ReSharper restore InconsistentNaming
            {
                get { return Variables["Pos.x"].SafeParse<float>(); }
                set { Variables["Pos.x"] = value; }
            }


// ReSharper disable InconsistentNaming
            public float Pos_y
// ReSharper restore InconsistentNaming
            {
                get { return Variables["Pos.y"].SafeParse<float>(); }
                set { Variables["Pos.y"] = value; }
            }

            public string Type
            {
                get { return Variables["Type"].SafeParse<string>(); }
                set { Variables["Type"] = value; }
            }

            public int SubType
            {
                get { return Variables["SubType"].SafeParse<int>(); }
                set { Variables["SubType"] = value; }
            }

            #endregion Variables
        }

        #endregion
    }
}