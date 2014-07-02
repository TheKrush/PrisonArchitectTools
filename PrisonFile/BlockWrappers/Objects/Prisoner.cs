using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.BlockWrappers.Base;
using PrisonArchitect.PrisonFile.BlockWrappers.Helper;

namespace PrisonArchitect.PrisonFile.BlockWrappers.Objects
{
    public class Prisoner : Character
    {
        public Prisoner(Block block) : base(block) { Cell = new Id(block, "Cell"); }

        #region Variables

        // ReSharper disable InconsistentNaming

        public float BoilingPoint { get { return Variables["BoilingPoint"].SafeParse<float>(); } set { Variables["BoilingPoint"] = value; } }

        public string Category { get { return Variables["Category"].SafeParse<string>(); } set { Variables["Category"] = value; } }

        public string RequiredCellType { get { return Variables["RequiredCellType"].SafeParse<string>(); } set { Variables["RequiredCellType"] = value; } }

        public bool Shackled { get { return Variables["Shackled"].SafeParse<bool>(); } set { Variables["Shackled"] = value; } }

        // ReSharper restore InconsistentNaming

        #endregion Variables

        public Id Cell { get; set; }
    }
}