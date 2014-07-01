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
            #region Nested type: HistoricalTracker

            public class HistoricalTracker : HandledBlock
            {
                #region Nested type: Log

                public class Log : HandledBlock
                {
                    #region Nested type: SubLog

                    public class SubLog : HandledBlock
                    {
                    }

                    #endregion
                }

                #endregion
            }

            #endregion
        }

        #endregion

        #region Nested type: Prisoners

        public class Prisoners : BlockList
        {
            #region Nested type: Prisoner

            public class Prisoner : HandledBlock
            {
            }

            #endregion
        }

        #endregion

        #region Nested type: Trackers

        public class Trackers : BlockList
        {
            #region Nested type: Tracker

            public class Tracker : HandledBlock
            {
                #region Nested type: Log

                public class Log : HandledBlock
                {
                    #region Nested type: SubLog

                    public class SubLog : HandledBlock
                    {
                    }

                    #endregion
                }

                #endregion
            }

            #endregion
        }

        #endregion
    }
}