using System;
using System.Collections.Generic;
using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.BlockWrappers.Helper;

namespace PrisonArchitect.PrisonFile.BlockWrappers
{
    public class ObjectBase : BlockWrapper
    {
        #region EType enum

        public enum EType
        {
            // ReSharper disable InconsistentNaming
            Accountant,
            Bed,
            Bench,
            Bin,
            Bookshelf,
            Box,
            Capacitor,
            CarpenterTable,
            Cctv,
            CctvMonitor,
            Chair,
            Chief,
            Cook,
            Cooker,
            Doctor,
            DogCrate,
            Door,
            Drain,
            DrinkMachine,
            ElectricChair,
            FilingCabinet,
            FoodTrayDirty,
            FoodWaste,
            Foreman,
            Fridge,
            Garbage,
            Gardener,
            Guard,
            GuardLocker,
            Ingredients,
            IroningBoard,
            JailDoor,
            JailDoorLarge,
            Janitor,
            LaundryBasket,
            LaundryMachine,
            Light,
            Log,
            MedicalBed,
            MetalDetector,
            MorgueSlab,
            OfficeDesk,
            PhoneBooth,
            PipeValve,
            PoolTable,
            PowerStation,
            PowerSwitch,
            Prisoner,
            PrisonerUniform,
            Psychologist,
            RoadGate,
            SchoolDesk,
            ServingTable,
            SheetMetal,
            ShowerHead,
            Sink,
            SofaChairDouble,
            SolitaryDoor,
            Stack,
            StaffDoor,
            SupplyTruck,
            Table,
            Toilet,
            Tree,
            TreeStump,
            Tv,
            Visitor,
            VisitorTable,
            Warden,
            WaterPumpStation,
            WeaponRack,
            WeightsBench,
            Workman,
            WorkshopPress,
            WorkshopSaw,
            // ReSharper restore InconsistentNaming
        }

        #endregion

        public ObjectBase(Block block) : base(block)
        {
            Id = new Id(block, "Id");
            CarrierId = new Id(block, "Carrier");

            Or = new Vector2D<float>(block, "Or");
            Pos = new Vector2D<float>(block, "Pos");
            Walls = new Vector2D<float>(block, "Walls");
        }

        public static IEnumerable<string> Types { get { return Enum.GetNames(typeof (EType)); } }

        public Id Id { get; set; }
        public Id CarrierId { get; set; }

        public Vector2D<float> Or { get; set; }
        public Vector2D<float> Pos { get; set; }
        public Vector2D<float> Walls { get; set; }

        #region Variables

        // ReSharper disable InconsistentNaming

        public float Damage { get { return Variables["Damage"].SafeParse<float>(); } set { Variables["Damage"] = value; } }

        public string Name { get { return Variables["Name"].SafeParse<string>(); } set { Variables["Name"] = value; } }

        public int SubType { get { return Variables["SubType"].SafeParse<int>(); } set { Variables["SubType"] = value; } }

        public float Timer { get { return Variables["Timer"].SafeParse<float>(); } set { Variables["Timer"] = value; } }

        public string Type { get { return Variables["Type"].SafeParse<string>(); } set { Variables["Type"] = value; } }

        // ReSharper restore InconsistentNaming

        #endregion Variables
    }
}