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
            #region Nested type: Program

            public class Program : HandledBlock
            {
                #region Nested type: Objects

                public class Objects : HandledBlock
                {
                    #region Nested type: Object

                    public class Object : Id
                    {
                    }

                    #endregion
                }

                #endregion

                #region Nested type: Students

                public class Students : HandledBlock
                {
                    #region Nested type: Student

                    public class Student : HandledBlock
                    {
                    }

                    #endregion
                }

                #endregion
            }

            #endregion
        }

        #endregion

        #region Nested type: Reports

        public class Reports : HandledBlock
        {
            #region Nested type: Base

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

            #endregion

            #region Nested type: Therapy

            public class Therapy : Base
            {
            }

            #endregion

            #region Nested type: WorkshopInduction

            public class WorkshopInduction : Base
            {
            }

            #endregion
        }

        #endregion
    }
}