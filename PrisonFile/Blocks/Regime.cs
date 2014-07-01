using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Regime : HandledBlock
    {
        #region Variables

        public int PreviousHour
        {
            get { return Variables["PreviousHour"].SafeParse<int>(); }
            set { Variables["PreviousHour"] = value; }
        }

        #endregion Variables

        #region Nested type: Base

        public class Base : HandledBlock
        {
            #region Variables

            public string CurrentActivity
            {
                get { return Variables["CurrentActivity"].SafeParse<string>(); }
                set { Variables["CurrentActivity"] = value; }
            }

            public string PreviousActivity
            {
                get { return Variables["PreviousActivity"].SafeParse<string>(); }
                set { Variables["PreviousActivity"] = value; }
            }

            #endregion Variables
        }

        #endregion

        #region Nested type: MaxSec

        public class MaxSec : Base
        {
        }

        #endregion

        #region Nested type: MinSec

        public class MinSec : Base
        {
        }

        #endregion

        #region Nested type: Normal

        public class Normal : Base
        {
        }

        #endregion
    }
}