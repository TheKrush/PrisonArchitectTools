using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.BlockWrappers.Helper;

namespace PrisonArchitect.PrisonFile.BlockWrappers.Objects
{
    public class SupplyTruck : ObjectBase
    {
        public SupplyTruck(Block block) : base(block)
        {
            ProcessingRoomId = new Id(block, "ProcessingRoomId");

            Slot = new[]
                   {
                       new Id(block, "Slot0"),
                       new Id(block, "Slot1"),
                       new Id(block, "Slot2"),
                       new Id(block, "Slot3"),
                       new Id(block, "Slot4"),
                       new Id(block, "Slot5"),
                       new Id(block, "Slot6"),
                       new Id(block, "Slot7"),
                   };
        }

        public Id ProcessingRoomId { get; set; }

        // TODO: possible change this into 8 variables
        public Id[] Slot { get; set; }

        #region Variables

        // ReSharper disable InconsistentNaming

        public float Speed { get { return Variables["Speed"].SafeParse<float>(); } set { Variables["Speed"] = value; } }

        public string State { get { return Variables["State"].SafeParse<string>(); } set { Variables["State"] = value; } }

        // ReSharper restore InconsistentNaming

        #endregion Variables
    }
}
