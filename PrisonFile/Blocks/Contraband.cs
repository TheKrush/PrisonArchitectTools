using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Contraband : HandledBlock
    {
        #region Variables

        public float Timer
        {
            get { return Variables["Timer"].SafeParse<float>(); }
            set { Variables["Timer"] = value; }
        }

        public int ObjectIndex
        {
            get { return Variables["ObjectIndex"].SafeParse<int>(); }
            set { Variables["ObjectIndex"] = value; }
        }

        #endregion Variables

        #region Nested type: HistoricalTrackers

        public class HistoricalTrackers : BlockList
        {
            public class HistoricalTracker : HandledBlock
            {
                public class Log : HandledBlock
                {
                    public class SubLog : HandledBlock
                    {

                    }
                }
            }
        }

        #endregion

        #region Nested type: Prisoners

        public class Prisoners : BlockList
        {
            public class Prisoner : HandledBlock
            {
                
            }
        }

        #endregion

        #region Nested type: Trackers

        public class Trackers : BlockList
        {
            public class Tracker : HandledBlock
            {
                public class Log : HandledBlock
                {
                    public class SubLog : HandledBlock
                    {
                        
                    }
                }
            }
        }

        #endregion
    }
}