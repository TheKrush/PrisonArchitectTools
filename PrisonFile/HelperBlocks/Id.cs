using PrisonArchitect.Helper;

namespace PrisonArchitect.PrisonFile.HelperBlocks
{
    public class Id : HandledBlock
    {
        #region Variables

        // ReSharper disable InconsistentNaming
        public int Id_i
            // ReSharper restore InconsistentNaming
        {
            get { return Variables["Id.i"].SafeParse<int>(); }
            set { Variables["Id.i"] = value; }
        }

        // ReSharper disable InconsistentNaming
        public int Id_u
            // ReSharper restore InconsistentNaming
        {
            get { return Variables["Id.u"].SafeParse<int>(); }
            set { Variables["Id.u"] = value; }
        }

        #endregion Variables
    }
}