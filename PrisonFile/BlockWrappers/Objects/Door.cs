using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.BlockWrappers.Helper;

namespace PrisonArchitect.PrisonFile.BlockWrappers.Objects
{
    public class Door : ObjectBase
    {
        public Door(Block block) : base(block)
        {
            CellId = new Id(block, "CellId");

            OpenDir = new Vector2D<float>(block, "OpenDir");
        }

        #region Variables

        // ReSharper disable InconsistentNaming

        public float CellIdTimer { get { return Variables["CellIdTimer"].SafeParse<float>(); } set { Variables["CellIdTimer"] = value; } }

        public int Changing { get { return Variables["Changing"].SafeParse<int>(); } set { Variables["Changing"] = value; } }

        public float CloseTimer { get { return Variables["CloseTimer"].SafeParse<float>(); } set { Variables["CloseTimer"] = value; } }

        public string Mode { get { return Variables["Mode"].SafeParse<string>(); } set { Variables["Mode"] = value; } }

        public float Open { get { return Variables["Open"].SafeParse<float>(); } set { Variables["Open"] = value; } }

        public float SectorTimer { get { return Variables["SectorTimer"].SafeParse<float>(); } set { Variables["SectorTimer"] = value; } }

        // ReSharper restore InconsistentNaming

        #endregion Variables

        public Id CellId { get; set; }

        public Vector2D<float> OpenDir { get; set; }
    }
}
