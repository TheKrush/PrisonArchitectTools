using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Thermometer : HandledBlock
    {
        #region Variables

        public float Temperature
        {
            get { return Variables["Temperature"].SafeParse<float>(); }
            set { Variables["Temperature"] = value; }
        }

        public float Desired
        {
            get { return Variables["Desired"].SafeParse<float>(); }
            set { Variables["Desired"] = value; }
        }

        public float RateOfChange
        {
            get { return Variables["RateOfChange"].SafeParse<float>(); }
            set { Variables["RateOfChange"] = value; }
        }

        public float HappyPrisoners
        {
            get { return Variables["HappyPrisoners"].SafeParse<float>(); }
            set { Variables["HappyPrisoners"] = value; }
        }

        public float UnhappyPrisoners
        {
            get { return Variables["UnhappyPrisoners"].SafeParse<float>(); }
            set { Variables["UnhappyPrisoners"] = value; }
        }

        public float SuppressedPrisoners
        {
            get { return Variables["SuppressedPrisoners"].SafeParse<float>(); }
            set { Variables["SuppressedPrisoners"] = value; }
        }

        public float RecentDeaths
        {
            get { return Variables["RecentDeaths"].SafeParse<float>(); }
            set { Variables["RecentDeaths"] = value; }
        }

        public float RecentPunishments
        {
            get { return Variables["RecentPunishments"].SafeParse<float>(); }
            set { Variables["RecentPunishments"] = value; }
        }

        public float RecentSearches
        {
            get { return Variables["RecentSearches"].SafeParse<float>(); }
            set { Variables["RecentSearches"] = value; }
        }

        #endregion Variables
    }
}