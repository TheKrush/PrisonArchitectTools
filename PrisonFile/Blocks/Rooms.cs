using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Rooms : BlockList
    {
        #region Nested type: Room

        public class Room : Id
        {
            #region Variables

            public string RoomType
            {
                get { return Variables["RoomType"].SafeParse<string>(); }
                set { Variables["RoomType"] = value; }
            }

            public string Name
            {
                get { return Variables["Name"].SafeParse<string>(); }
                set { Variables["Name"] = value; }
            }

            #endregion Variables
        }

        #endregion
    }
}