using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Misconduct : HandledBlock
    {
        #region Variables

        public float TimeWithoutIncident
        {
            get { return Variables["TimeWithoutIncident"].SafeParse<float>(); }
            set { Variables["TimeWithoutIncident"] = value; }
        }

        #endregion Variables

        #region Nested type: MisconductReports

        public class MisconductReports : BlockList
        {
            #region Nested type: MisconductReport

            public class MisconductReport : HandledBlock
            {
                #region Nested type: MisconductEntries

                public class MisconductEntries : BlockList
                {
                    #region Nested type: MisconductEntry

                    public class MisconductEntry : HandledBlock
                    {
                    }

                    #endregion
                }

                #endregion
            }

            #endregion
        }

        #endregion

        #region Nested type: Policy

        public class Policy : HandledBlock
        {
            #region Nested type: Base

            public class Base : HandledBlock
            {
                #region Variables

                public string Punishment
                {
                    get { return Variables["Punishment"].SafeParse<string>(); }
                    set { Variables["Punishment"] = value; }
                }

                public int Quantity
                {
                    get { return Variables["Quantity"].SafeParse<int>(); }
                    set { Variables["Quantity"] = value; }
                }

                public bool SearchCell
                {
                    get { return Variables["SearchCell"].SafeParse<bool>(); }
                    set { Variables["SearchCell"] = value; }
                }

                #endregion Variables
            }

            #endregion

            #region Nested type: Complaint

            public class Complaint : HandledBlock
            {
            }

            #endregion

            #region Nested type: ContrabandLuxuries

            public class ContrabandLuxuries : Base
            {
            }

            #endregion

            #region Nested type: ContrabandNarcotics

            public class ContrabandNarcotics : Base
            {
            }

            #endregion

            #region Nested type: ContrabandTools

            public class ContrabandTools : Base
            {
            }

            #endregion

            #region Nested type: ContrabandWeapons

            public class ContrabandWeapons : Base
            {
            }

            #endregion

            #region Nested type: Destruction

            public class Destruction : Base
            {
            }

            #endregion

            #region Nested type: EscapeAttempt

            public class EscapeAttempt : Base
            {
            }

            #endregion

            #region Nested type: InjuredPrisoner

            public class InjuredPrisoner : Base
            {
            }

            #endregion

            #region Nested type: InjuredStaff

            public class InjuredStaff : Base
            {
            }

            #endregion

            #region Nested type: Murder

            public class Murder : HandledBlock
            {
            }

            #endregion

            #region Nested type: None

            public class None : HandledBlock
            {
            }

            #endregion

            #region Nested type: SeriousInjury

            public class SeriousInjury : Base
            {
            }

            #endregion
        }

        #endregion
    }
}