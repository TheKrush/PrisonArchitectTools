using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Reform : HandledBlock
    {
        #region Variables

        public int NextProgramId
        {
            get { return Variables["NextProgramId"].SafeParse<int>(); }
            set { Variables["NextProgramId"] = value; }
        }

        #endregion Variables

        #region Nested type: Allocations

        public class Allocations : HandledBlock
        {
        }

        #endregion

        #region Nested type: Programs

        public class Programs : BlockList
        {
            public class Program : HandledBlock
            {
                public class Objects : HandledBlock
                {
                    public class Object : Id
                    {
                        
                    }
                }

                public class Students : HandledBlock
                {
                    public class Student : HandledBlock
                    {
                    }
                }
            }
        }

        #endregion

        #region Nested type: Reports

        public class Reports : HandledBlock
        {
            public class Base : HandledBlock
            {
                #region Variables

                public int Started
                {
                    get { return Variables["Started"].SafeParse<int>(); }
                    set { Variables["Started"] = value; }
                }

                public int Finished
                {
                    get { return Variables["Finished"].SafeParse<int>(); }
                    set { Variables["Finished"] = value; }
                }

                public int Passed
                {
                    get { return Variables["Passed"].SafeParse<int>(); }
                    set { Variables["Passed"] = value; }
                }

                #endregion Variables
            }

            public class WorkshopInduction : Base
            {
                
            }

            public class Therapy : Base
            {
                 
            }
        }

        #endregion
    }
}