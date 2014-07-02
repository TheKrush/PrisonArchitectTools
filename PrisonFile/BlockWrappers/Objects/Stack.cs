using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.BlockWrappers.Helper;

namespace PrisonArchitect.PrisonFile.BlockWrappers.Objects
{
    public class Stack : ObjectBase
    {
        public Stack(Block block) : base(block) { Carrier = new Id(block, "Carrier"); }

        public Id Carrier { get; set; }

        #region Variables

        // ReSharper disable InconsistentNaming

        public bool Loaded { get { return Variables["Loaded"].SafeParse<bool>(); } set { Variables["Loaded"] = value; } }

        public string Contents { get { return Variables["Contents"].SafeParse<string>(); } set { Variables["Contents"] = value; } }

        public int Quantity { get { return Variables["Quantity"].SafeParse<int>(); } set { Variables["Quantity"] = value; } }

        // ReSharper restore InconsistentNaming

        #endregion Variables
    }
}