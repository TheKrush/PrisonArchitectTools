using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Visitation : HandledBlock
    {
        #region Variables

        public float Timer
        {
            get { return Variables["Timer"].SafeParse<float>(); }
            set { Variables["Timer"] = value; }
        }

        #endregion Variables

        public class SubVisitation : HandledBlock
        {
            
        }
    }
}