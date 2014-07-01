using System;
using System.Collections.Generic;
using System.Linq;
using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Cells : HandledBlock
    {
        public Cell GetCell(string key)
        {
            return Blocks.Find(block => block.BlockName == key) as Cell;
        }

        #region Nested type: Cell

        public class Cell : HelperBlocks.Cell
        {
            #region EMaterial enum

            public enum EMaterial
            {
// ReSharper disable InconsistentNaming
                BrickWall,
                CeramicFloor,
                ConcreteFloor,
                ConcreteTiles,
                ConcreteWall,
                Dirt,
                FancyTiles,
                Fence,
                Grass,
                Gravel,
                LongGrass,
                MarbleTiles,
                MetalFloor,
                MosaicFloor,
                PavingStone,
                PerimeterWall,
                Road,
                RoadMarkings,
                RoadMarkingsLeft,
                RoadMarkingsRight,
                Sand,
                Stone,
                Water,
                WhiteTiles,
                WoodenFloor,
// ReSharper restore InconsistentNaming
            }

            #endregion

            public static List<string> Materials
            {
                get { return Enum.GetNames(typeof (EMaterial)).ToList(); }
            }

            #region Variables

            public string Mat
            {
                get
                {
                    try
                    {
                        return Variables["Mat"].SafeParse<string>();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                set
                {
                    value = string.IsNullOrEmpty(value) ? "Dirt" : value;

                    EMaterial material = (EMaterial) Enum.Parse(typeof (EMaterial), value);
                    switch (material)
                    {
                        case EMaterial.CeramicFloor:
                        case EMaterial.ConcreteFloor:
                        case EMaterial.FancyTiles:
                        case EMaterial.MarbleTiles:
                        case EMaterial.MetalFloor:
                        case EMaterial.MosaicFloor:
                        case EMaterial.WhiteTiles:
                        case EMaterial.WoodenFloor:
                            Indoors = true;
                            break;

                        case EMaterial.Dirt:
                        case EMaterial.Fence:
                        case EMaterial.Grass:
                        case EMaterial.Gravel:
                        case EMaterial.LongGrass:
                        case EMaterial.PerimeterWall:
                        case EMaterial.Road:
                        case EMaterial.RoadMarkings:
                        case EMaterial.RoadMarkingsLeft:
                        case EMaterial.RoadMarkingsRight:
                        case EMaterial.Sand:
                        case EMaterial.Stone:
                        case EMaterial.Water:
                            Indoors = false;
                            break;
                    }

                    // dirt is the absence of a material variable
                    if (value == "Dirt") value = null;
                    Variables["Mat"] = value;
                }
            }

            public string Material
            {
                get { return Mat; }
                set { Mat = value; }
            }

            public float Con
            {
                get { return Variables["Con"].SafeParse<float>(); }
                set { Variables["Con"] = value; }
            }

            public float Condition
            {
                get { return Con; }
                set { Con = value; }
            }

// ReSharper disable InconsistentNaming
            public int Room_i
// ReSharper restore InconsistentNaming
            {
                get { return Variables["Room.i"].SafeParse<int>(); }
                set { Variables["Room.i"] = value; }
            }


// ReSharper disable InconsistentNaming
            public int Room_u
// ReSharper restore InconsistentNaming
            {
                get { return Variables["Room.u"].SafeParse<int>(); }
                set { Variables["Room.u"] = value; }
            }

            public bool Ind
            {
                get { return Variables["Ind"].SafeParse<bool>(); }
                set { Variables["Ind"] = value; }
            }

            public bool Indoors
            {
                get { return Ind; }
                set { Ind = value; }
            }

            #endregion Variables
        }

        #endregion
    }
}