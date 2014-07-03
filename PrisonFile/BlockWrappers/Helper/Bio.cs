using System;
using System.Drawing;
using System.Globalization;
using PrisonArchitect.Helper;

namespace PrisonArchitect.PrisonFile.BlockWrappers.Helper
{
    public class Bio : HelperBase
    {
        public Bio(Block block, string name) : base(block, name) { }

        #region Variables

        // ReSharper disable InconsistentNaming

        public float Age { get { return Variables["Age"].SafeParse<float>(); } set { Variables["Age"] = value; } }

        public float BodyScale { get { return Variables["BodyScale"].SafeParse<float>(); } set { Variables["BodyScale"] = value; } }

        public int BodyType { get { return Variables["BodyType"].SafeParse<int>(); } set { Variables["BodyType"] = value; } }

        public Color ClothingColour { get { return HexToColor(Variables["ClothingColour"].SafeParse<string>()); } set { Variables["ClothingColour"] = ColorToHex(value); } }

        public string Forname { get { return Variables["Forname"].SafeParse<string>(); } set { Variables["Forname"] = value; } }

        public string HeadType { get { return Variables["HeadType"].SafeParse<string>(); } set { Variables["HeadType"] = value; } }

        public int Parole { get { return Variables["Parole"].SafeParse<int>(); } set { Variables["Parole"] = value; } }

        public int Sentence { get { return Variables["Sentence"].SafeParse<int>(); } set { Variables["Sentence"] = value; } }

        public float Served { get { return Variables["Served"].SafeParse<float>(); } set { Variables["Served"] = value; } }

        public Color SkinColour { get { return HexToColor(Variables["SkinColour"].SafeParse<string>()); } set { Variables["SkinColour"] = ColorToHex(value); } }

        public string Surname { get { return Variables["Surname"].SafeParse<string>(); } set { Variables["Surname"] = value; } }

        public string Traits { get { return Variables["Traits"].SafeParse<string>(); } set { Variables["Traits"] = value; } }

        public string Type { get { return Variables["Type"].SafeParse<string>(); } set { Variables["Type"] = value; } }

        // ReSharper restore InconsistentNaming

        #endregion Variables

        #region Color Conversion

        public static Color HexToColor(string hexColor)
        {
            //Remove # if present
            if (hexColor.IndexOf("0x", StringComparison.Ordinal) != -1)
                hexColor = hexColor.Replace("0x", "");

            //#RRGGBBAA
            int red = int.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            int green = int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            int blue = int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            int alpha = int.Parse(hexColor.Substring(6, 2), NumberStyles.AllowHexSpecifier);

            return Color.FromArgb(alpha, red, green, blue);
        }

        public static string ColorToHex(Color color)
        {
            //#RRGGBBAA
            return String.Format("0x{0}{1}{2}{3}",
                                 color.R.ToString("X").Length == 1 ? String.Format("0{0}", color.R.ToString("X")) : color.R.ToString("X"),
                                 color.G.ToString("X").Length == 1 ? String.Format("0{0}", color.G.ToString("X")) : color.G.ToString("X"),
                                 color.B.ToString("X").Length == 1 ? String.Format("0{0}", color.B.ToString("X")) : color.B.ToString("X"),
                                 color.A.ToString("X").Length == 1 ? String.Format("0{0}", color.A.ToString("X")) : color.A.ToString("X"));
        }

        #endregion Color Conversion
    }
}
