using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Penalties : HandledBlock
    {
        #region Variables

        public float Points
        {
            get { return Variables["Points"].SafeParse<float>(); }
            set { Variables["Points"] = value; }
        }

        #endregion Variables

        #region Nested type: SubPenalties

        public class SubPenalties : BlockList
        {
            #region Nested type: Item

            public class Item : HandledBlock
            {
                #region Variables

                public int Event
                {
                    get { return Variables["Event"].SafeParse<int>(); }
                    set { Variables["Event"] = value; }
                }

// ReSharper disable InconsistentNaming
                public int ObjectId_i
// ReSharper restore InconsistentNaming
                {
                    get { return Variables["ObjectId.i"].SafeParse<int>(); }
                    set { Variables["ObjectId.i"] = value; }
                }

// ReSharper disable InconsistentNaming
                public int ObjectId_u
// ReSharper restore InconsistentNaming
                {
                    get { return Variables["ObjectId.u"].SafeParse<int>(); }
                    set { Variables["ObjectId.u"] = value; }
                }

                public float Time
                {
                    get { return Variables["Time"].SafeParse<float>(); }
                    set { Variables["Time"] = value; }
                }

                public bool Committed
                {
                    get { return Variables["Committed"].SafeParse<bool>(); }
                    set { Variables["Committed"] = value; }
                }

                #endregion Variables
            }

            #endregion
        }

        #endregion
    }
}