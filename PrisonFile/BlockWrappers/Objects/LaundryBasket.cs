using PrisonArchitect.PrisonFile.BlockWrappers.Helper;

namespace PrisonArchitect.PrisonFile.BlockWrappers.Objects
{
    public class LaundryBasket : ObjectBase
    {
        public LaundryBasket(Block block) : base(block) { HomeSector = new Id(block, "HomeSector"); }

        #region Variables

        // ReSharper disable InconsistentNaming

        // ReSharper restore InconsistentNaming

        #endregion Variables

        public Id HomeSector { get; set; }
    }
}
