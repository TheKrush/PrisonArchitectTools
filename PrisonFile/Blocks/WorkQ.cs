using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class WorkQ : HandledBlock
    {
        #region Variables

        public int Next
        {
            get { return Variables["Next"].SafeParse<int>(); }
            set { Variables["Next"] = value; }
        }

        #endregion Variables

        #region Nested type: Items

        public class Items : BlockList
        {
            #region Nested type: Item

            public class Item : HandledBlock
            {
                #region Variables

                public int Id
                {
                    get { return Variables["Id"].SafeParse<int>(); }
                    set { Variables["Id"] = value; }
                }

                public string Type
                {
                    get { return Variables["Type"].SafeParse<string>(); }
                    set { Variables["Type"] = value; }
                }

                public int CellX
                {
                    get { return Variables["CellX"].SafeParse<int>(); }
                    set { Variables["CellX"] = value; }
                }

                public int CellY
                {
                    get { return Variables["CellY"].SafeParse<int>(); }
                    set { Variables["CellY"] = value; }
                }

                public string ObjType
                {
                    get { return Variables["ObjType"].SafeParse<string>(); }
                    set { Variables["ObjType"] = value; }
                }

                public bool InProg
                {
                    get { return Variables["InProg"].SafeParse<bool>(); }
                    set { Variables["InProg"] = value; }
                }

                public float WorkDone
                {
                    get { return Variables["WorkDone"].SafeParse<float>(); }
                    set { Variables["WorkDone"] = value; }
                }

                public float WorkTotal
                {
                    get { return Variables["WorkTotal"].SafeParse<float>(); }
                    set { Variables["WorkTotal"] = value; }
                }

                #endregion Variables
            }

            #endregion
        }

        #endregion
    }
}