using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Construction : HandledBlock
    {
        #region Nested type: BlockedAreas

        public class BlockedAreas : BlockList
        {
        }

        #endregion

        #region Nested type: Jobs

        public class Jobs : BlockList
        {
        }

        #endregion

        #region Nested type: PlanningJobs

        public class PlanningJobs : BlockList
        {
            public class Job : HandledBlock
            {
                
            }
        }

        #endregion
    }
}