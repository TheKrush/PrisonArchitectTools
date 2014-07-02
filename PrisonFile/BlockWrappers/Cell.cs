using System;
using System.Collections.Generic;
using PrisonArchitect.Helper;

namespace PrisonArchitect.PrisonFile.BlockWrappers
{
    public class Cell : BlockWrapper
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

        public Cell(Block block) : base(block) { }

        public static IEnumerable<string> Materials { get { return Enum.GetNames(typeof (EMaterial)); } }

        public int X { get { return Block.BlockName.Replace("\"", "").Split(null)[0].SafeParse<int>(); } }

        public int Y { get { return Block.BlockName.Replace("\"", "").Split(null)[1].SafeParse<int>(); } }

        public string Material { get { return Mat; } set { Mat = value; } }

        public float Condition { get { return Con; } set { Con = value; } }

        public bool Indoors { get { return Ind; } set { Ind = value; } }

        #region Static

        public static bool IsAlwaysIndoor(string mat)
        {
            string material = mat;
            material = string.IsNullOrEmpty(material) ? "Dirt" : material;
            return IsAlwaysIndoor((EMaterial) Enum.Parse(typeof (EMaterial), material));
        }

        public static bool IsAlwaysIndoor(EMaterial material)
        {
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
                    return true;
            }
            return false;
        }

        public static bool IsAlwaysOutdoor(string mat)
        {
            string material = mat;
            material = string.IsNullOrEmpty(material) ? "Dirt" : material;
            return IsAlwaysOutdoor((EMaterial) Enum.Parse(typeof (EMaterial), material));
        }

        public static bool IsAlwaysOutdoor(EMaterial material)
        {
            switch (material)
            {
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
                    return true;
            }
            return false;
        }

        #endregion Static

        #region Variables

        // ReSharper disable InconsistentNaming

        public string Mat
        {
            // Dirt is a special case
            get
            {
                string material = Variables["Mat"].SafeParse<string>();
                return string.IsNullOrEmpty(material) ? "Dirt" : material;
            }
            set
            {
                value = string.IsNullOrEmpty(value) ? "Dirt" : value;

                if (IsAlwaysIndoor(value)) Indoors = true;
                else if (IsAlwaysOutdoor(value)) Indoors = false;

                // dirt is the absence of a material variable
                if (value == "Dirt") value = null;
                Variables["Mat"] = value;
            }
        }

        public float Con { get { return Variables["Con"].SafeParse<float>(); } set { Variables["Con"] = value; } }

        public int Room_i { get { return Variables["Room.i"].SafeParse<int>(); } set { Variables["Room.i"] = value; } }

        public int Room_u { get { return Variables["Room.u"].SafeParse<int>(); } set { Variables["Room.u"] = value; } }

        public bool Ind { get { return Variables["Ind"].SafeParse<bool>(); } set { Variables["Ind"] = value; } }

        // ReSharper restore InconsistentNaming

        #endregion Variables
    }
}
