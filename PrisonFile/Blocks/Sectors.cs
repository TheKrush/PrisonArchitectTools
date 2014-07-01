using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Sectors : HandledBlock
    {
        #region Variables

        public float NextSectorId
        {
            get { return Variables["NextSectorId"].SafeParse<float>(); }
            set { Variables["NextSectorId"] = value; }
        }

        #endregion Variables

        #region Nested type: SubSectors

        public class SubSectors : BlockList
        {
            #region Nested type: Sector

            public class Sector : HandledBlock
            {
                #region Variables

// ReSharper disable InconsistentNaming
                public int id
// ReSharper restore InconsistentNaming
                {
                    get { return Variables["id"].SafeParse<int>(); }
                    set { Variables["id"] = value; }
                }

// ReSharper disable InconsistentNaming
                public float TopLeft_x
// ReSharper restore InconsistentNaming
                {
                    get { return Variables["TopLeft.x"].SafeParse<float>(); }
                    set { Variables["TopLeft.x"] = value; }
                }

// ReSharper disable InconsistentNaming
                public float TopLeft_y
// ReSharper restore InconsistentNaming
                {
                    get { return Variables["TopLeft.y"].SafeParse<float>(); }
                    set { Variables["TopLeft.y"] = value; }
                }

// ReSharper disable InconsistentNaming
                public float BottomRight_x
// ReSharper restore InconsistentNaming
                {
                    get { return Variables["BottomRight.x"].SafeParse<float>(); }
                    set { Variables["BottomRight.x"] = value; }
                }

// ReSharper disable InconsistentNaming
                public float BottomRight_y
// ReSharper restore InconsistentNaming
                {
                    get { return Variables["BottomRight.y"].SafeParse<float>(); }
                    set { Variables["BottomRight.y"] = value; }
                }

// ReSharper disable InconsistentNaming
                public float Centre_x
// ReSharper restore InconsistentNaming
                {
                    get { return Variables["Centre.x"].SafeParse<float>(); }
                    set { Variables["Centre.x"] = value; }
                }

// ReSharper disable InconsistentNaming
                public float Centre_y
// ReSharper restore InconsistentNaming
                {
                    get { return Variables["Centre.y"].SafeParse<float>(); }
                    set { Variables["Centre.y"] = value; }
                }

                public int NumSquares
                {
                    get { return Variables["NumSquares"].SafeParse<int>(); }
                    set { Variables["NumSquares"] = value; }
                }

                public int NumFloorSquares
                {
                    get { return Variables["NumFloorSquares"].SafeParse<int>(); }
                    set { Variables["NumFloorSquares"] = value; }
                }

                public bool Indoor
                {
                    get { return Variables["Indoor"].SafeParse<bool>(); }
                    set { Variables["Indoor"] = value; }
                }

                public bool Zone
                {
                    get { return Variables["Zone"].SafeParse<bool>(); }
                    set { Variables["Zone"] = value; }
                }

                #endregion Variables

                #region Nested type: Jobs

                public class Jobs : BlockList
                {
                    #region Nested type: Job

                    public class Job : HandledBlock
                    {
                        #region Variables

                        public float LastOccupied
                        {
                            get { return Variables["LastOccupied"].SafeParse<float>(); }
                            set { Variables["LastOccupied"] = value; }
                        }

// ReSharper disable InconsistentNaming
                        public int Entity_i
// ReSharper restore InconsistentNaming
                        {
                            get { return Variables["Entity.i"].SafeParse<int>(); }
                            set { Variables["Entity.i"] = value; }
                        }

// ReSharper disable InconsistentNaming
                        public float Entity_u
// ReSharper restore InconsistentNaming
                        {
                            get { return Variables["Entity.u"].SafeParse<float>(); }
                            set { Variables["Entity.u"] = value; }
                        }

                        #endregion Variables
                    }

                    #endregion
                }

                #endregion

                #region Nested type: Stations

                public class Stations : BlockList
                {
                    #region Nested type: Station

                    public class Station : HandledBlock
                    {
                        #region Variables

                        public float LastOccupied
                        {
                            get { return Variables["LastOccupied"].SafeParse<float>(); }
                            set { Variables["LastOccupied"] = value; }
                        }

                        // ReSharper disable InconsistentNaming
                        public int Entity_i
                            // ReSharper restore InconsistentNaming
                        {
                            get { return Variables["Entity.i"].SafeParse<int>(); }
                            set { Variables["Entity.i"] = value; }
                        }

                        // ReSharper disable InconsistentNaming
                        public float Entity_u
                            // ReSharper restore InconsistentNaming
                        {
                            get { return Variables["Entity.u"].SafeParse<float>(); }
                            set { Variables["Entity.u"] = value; }
                        }

                        #endregion Variables
                    }

                    #endregion
                }

                #endregion
            }

            #endregion
        }

        #endregion
    }
}