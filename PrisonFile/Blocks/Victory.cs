using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Victory : HandledBlock
    {
        #region Nested type: Conditions

        public class Conditions : HandledBlock
        {
            #region Nested type: Bankrupt

            public class Bankrupt : HandledBlock
            {
            }

            #endregion

            #region Nested type: Base

            public class Base : HandledBlock
            {
                #region Variables

                public int Value
                {
                    get { return Variables["Value"].SafeParse<int>(); }
                    set { Variables["Value"] = value; }
                }

                #endregion Variables
            }

            #endregion

            #region Nested type: Income

            public class Income : Base
            {
            }

            #endregion

            #region Nested type: PrisonValue

            public class PrisonValue : Base
            {
            }

            #endregion

            #region Nested type: Prisoners

            public class Prisoners : Base
            {
            }

            #endregion

            #region Nested type: PrisonersDied

            public class PrisonersDied : Base
            {
            }

            #endregion

            #region Nested type: PrisonersEscaped

            public class PrisonersEscaped : Base
            {
            }

            #endregion

            #region Nested type: PrisonersReleased

            public class PrisonersReleased : Base
            {
            }

            #endregion

            #region Nested type: Riot

            public class Riot : HandledBlock
            {
            }

            #endregion

            #region Nested type: TooManyDeaths

            public class TooManyDeaths : HandledBlock
            {
            }

            #endregion

            #region Nested type: TooManyEscapes

            public class TooManyEscapes : HandledBlock
            {
            }

            #endregion
        }

        #endregion

        #region Nested type: Log

        public class Log : HandledBlock
        {
            #region Variables

            public int Size
            {
                get { return Blocks.Count; }
            }

            #endregion Variables

            #region Nested type: SubLog

            public class SubLog : HandledBlock
            {
                #region Variables

                public string Type
                {
                    get { return Variables["Type"].SafeParse<string>(); }
                    set { Variables["Type"] = value; }
                }

                public float TimeIndex
                {
                    get { return Variables["TimeIndex"].SafeParse<float>(); }
                    set { Variables["TimeIndex"] = value; }
                }

                #endregion Variables
            }

            #endregion
        }

        #endregion

        #region Variables

// ReSharper disable InconsistentNaming
        public float RecentDeath_x
// ReSharper restore InconsistentNaming
        {
            get { return Variables["RecentDeath.x"].SafeParse<float>(); }
            set { Variables["RecentDeath.x"] = value; }
        }

// ReSharper disable InconsistentNaming
        public float RecentDeath_y
// ReSharper restore InconsistentNaming
        {
            get { return Variables["RecentDeath.y"].SafeParse<float>(); }
            set { Variables["RecentDeath.y"] = value; }
        }

        #endregion Variables
    }
}