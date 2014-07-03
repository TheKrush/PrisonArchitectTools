using PrisonArchitect.Helper;

namespace PrisonArchitect.PrisonFile.BlockWrappers.Helper
{
    public class Intake : HelperBase
    {
        public Intake(Block block, string name) : base(block, name) { }

        #region Variables

        // ReSharper disable InconsistentNaming

        public float next { get { return Variables[Name + ".next"].SafeParse<float>(); } set { Variables[Name + ".next"] = value; } }

        public int numPrisoners { get { return Variables[Name + ".numPrisoners"].SafeParse<int>(); } set { Variables[Name + ".numPrisoners"] = value; } }

        public bool reqMax { get { return Variables[Name + ".reqMax"].SafeParse<bool>(); } set { Variables[Name + ".reqMax"] = value; } }

        public bool reqMin { get { return Variables[Name + ".reqMin"].SafeParse<bool>(); } set { Variables[Name + ".reqMin"] = value; } }

        public bool reqNormal { get { return Variables[Name + ".reqNormal"].SafeParse<bool>(); } set { Variables[Name + ".reqNormal"] = value; } }

        // ReSharper restore InconsistentNaming

        #endregion Variables
    }
}
