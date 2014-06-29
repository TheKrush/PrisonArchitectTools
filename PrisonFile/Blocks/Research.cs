using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Research : HandledBlock
    {
        #region Nested type: Armoury

        public class Armoury : Base
        {
        }

        #endregion

        #region Nested type: BankLoans

        public class BankLoans : Base
        {
        }

        #endregion

        #region Nested type: Base

        public class Base : HandledBlock
        {
            #region Variables

            public bool Desired
            {
                get { return Variables["Desired"].SafeParse<bool>(); }
                set { Variables["Desired"] = value; }
            }

            public float Progress
            {
                get { return Variables["Progress"].SafeParse<float>(); }
                set { Variables["Progress"] = value; }
            }

            #endregion Variables
        }

        #endregion

        #region Nested type: BodyArmour

        public class BodyArmour : Base
        {
        }

        #endregion

        #region Nested type: Cctv

        public class Cctv : Base
        {
        }

        #endregion

        #region Nested type: Cleaning

        public class Cleaning : Base
        {
        }

        #endregion

        #region Nested type: Clone

        public class Clone : Base
        {
        }

        #endregion

        #region Nested type: Contraband

        public class Contraband : Base
        {
        }

        #endregion

        #region Nested type: Deployment

        public class Deployment : Base
        {
        }

        #endregion

        #region Nested type: Dogs

        public class Dogs : Base
        {
        }

        #endregion

        #region Nested type: Education

        public class Education : Base
        {
        }

        #endregion

        #region Nested type: ExtraGrant

        public class ExtraGrant : Base
        {
        }

        #endregion

        #region Nested type: Finance

        public class Finance : Base
        {
        }

        #endregion

        #region Nested type: GroundsKeeping

        public class GroundsKeeping : Base
        {
        }

        #endregion

        #region Nested type: Health

        public class Health : Base
        {
        }

        #endregion

        #region Nested type: LandExpansion

        public class LandExpansion : Base
        {
        }

        #endregion

        #region Nested type: LowerTaxes1

        public class LowerTaxes1 : Base
        {
        }

        #endregion

        #region Nested type: LowerTaxes2

        public class LowerTaxes2 : Base
        {
        }

        #endregion

        #region Nested type: Maintainance

        public class Maintainance : Base
        {
            // this class is misspelled in the save file so I also misspelled it here to match
        }

        #endregion

        #region Nested type: MentalHealth

        public class MentalHealth : Base
        {
        }

        #endregion

        #region Nested type: None

        public class None : Base
        {
        }

        #endregion

        #region Nested type: Patrols

        public class Patrols : Base
        {
        }

        #endregion

        #region Nested type: Policy

        public class Policy : Base
        {
        }

        #endregion

        #region Nested type: PrisonLabour

        public class PrisonLabour : Base
        {
        }

        #endregion

        #region Nested type: Security

        public class Security : Base
        {
        }

        #endregion

        #region Nested type: Tazers

        public class Tazers : Base
        {
        }

        #endregion

        #region Nested type: TazersForEveryone

        public class TazersForEveryone : Base
        {
        }

        #endregion

        #region Nested type: Warden

        public class Warden : Base
        {
        }

        #endregion
    }
}