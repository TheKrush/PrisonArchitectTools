using System;
using System.Collections.Generic;
using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.BlockWrappers.Helper;

namespace PrisonArchitect.PrisonFile.BlockWrappers.Base
{
    public class Character : ObjectBase
    {
        #region ECharacterType enum

        public enum ECharacterType
        {
            // ReSharper disable InconsistentNaming
            Accountant,
            Chief,
            Cook,
            Doctor,
            Foreman,
            Gardener,
            Guard,
            Janitor,
            //Prisoner,
            Psychologist,
            Visitor,
            Warden,
            Workman,
            // ReSharper restore InconsistentNaming
        }

        #endregion

        // gang.id
        public Character(Block block) : base(block)
        {
            Bio = new Bio(block, "Bio");

            AssignedRoom = new Id(block, "AssignedRoom");
            Carrying = new Id(block, "Carrying");
            Office = new Id(block, "Office");
            Station = new Id(block, "Station");
            TargetObject = new Id(block, "TargetObject");

            Vel = new Vector2D<float>(block, "Vel");
            Dest = new Vector2D<float>(block, "Dest");
        }

        #region Variables

        // ReSharper disable InconsistentNaming

        public float AiWalkSpeed { get { return Variables["AiWalkSpeed"].SafeParse<float>(); } set { Variables["AiWalkSpeed"] = value; } }

        public float AttackTimer { get { return Variables["AttackTimer"].SafeParse<float>(); } set { Variables["AttackTimer"] = value; } }

        public string BodyArmour { get { return Variables["BodyArmour"].SafeParse<string>(); } set { Variables["BodyArmour"] = value; } }

        public float Energy { get { return Variables["Energy"].SafeParse<float>(); } set { Variables["Energy"] = value; } }

        public string Equipment { get { return Variables["Equipment"].SafeParse<string>(); } set { Variables["Equipment"] = value; } }

        public bool HasKeys { get { return Variables["HasKeys"].SafeParse<bool>(); } set { Variables["HasKeys"] = value; } }

        public int JobId { get { return Variables["JobId"].SafeParse<int>(); } set { Variables["JobId"] = value; } }

        public bool Loaded { get { return Variables["Loaded"].SafeParse<bool>(); } set { Variables["Loaded"] = value; } }

        public string RestState { get { return Variables["RestState"].SafeParse<string>(); } set { Variables["RestState"] = value; } }

        public string SecondaryEquipment { get { return Variables["SecondaryEquipment"].SafeParse<string>(); } set { Variables["SecondaryEquipment"] = value; } }

        // ReSharper restore InconsistentNaming

        #endregion Variables

        public static IEnumerable<string> CharacterTypes { get { return Enum.GetNames(typeof (ECharacterType)); } }

        public Bio Bio { get; set; }

        public Id AssignedRoom { get; set; }
        public Id Carrying { get; set; }
        public Id Office { get; set; }
        public Id Station { get; set; }
        public Id TargetObject { get; set; }

        public Vector2D<float> Vel { get; set; }
        public Vector2D<float> Dest { get; set; }

        /*
        Gang.id : 0
        */
    }
}
