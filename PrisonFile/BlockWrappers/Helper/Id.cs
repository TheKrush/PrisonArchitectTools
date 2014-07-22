using System;
using System.Text;
using PrisonArchitect.Helper;

namespace PrisonArchitect.PrisonFile.BlockWrappers.Helper
{
    public class Id : HelperBase
    {
        public Id(Block block, string name) : base(block, name) { }

        #region Variables

        // ReSharper disable InconsistentNaming

        public int i { get { return Variables[Name + ".i"].SafeParse<int>(); } set { Variables[Name + ".i"] = value; } }

        public int next { get { return Variables[Name + ".next"].SafeParse<int>(); } set { Variables[Name + ".next"] = value; } }

        public int u { get { return Variables[Name + ".u"].SafeParse<int>(); } set { Variables[Name + ".u"] = value; } }

        // ReSharper restore InconsistentNaming

        #endregion Variables
    }
}
