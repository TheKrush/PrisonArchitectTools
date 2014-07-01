using System;
using System.Drawing;
using System.Globalization;
using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Objects : BlockList
    {
        #region Nested type: Human

        public class Human : Living
        {
            #region Variables

            public string HeadType
            {
                get { return Variables["HeadType"].SafeParse<string>(); }
                set { Variables["HeadType"] = value; }
            }

            public int BodyType
            {
                get { return Variables["BodyType"].SafeParse<int>(); }
                set { Variables["BodyType"] = value; }
            }

            public float BodyScale
            {
                get { return Variables["BodyScale"].SafeParse<float>(); }
                set { Variables["BodyScale"] = value; }
            }

            public Color SkinColour
            {
                get { return HexToColor(Variables["SkinColour"].SafeParse<string>()); }
                set { Variables["SkinColour"] = ColorToHex(value); }
            }

            #endregion Variables

            #region Color Conversion

            public static Color HexToColor(string hexColor)
            {
                //Remove # if present
                if (hexColor.IndexOf("0x", StringComparison.Ordinal) != -1)
                    hexColor = hexColor.Replace("0x", "");

                int red = 0;
                int green = 0;
                int blue = 0;
                int alpha = 0;

                //#RRGGBBAA
                red = int.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                green = int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                blue = int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
                alpha = int.Parse(hexColor.Substring(6, 2), NumberStyles.AllowHexSpecifier);

                return Color.FromArgb(alpha, red, green, blue);
            }

            public static string ColorToHex(Color color)
            {
                //#RRGGBBAA
                return String.Format("0x{0}{1}{2}{3}"
                                     ,
                                     color.R.ToString("X").Length == 1
                                         ? String.Format("0{0}", color.R.ToString("X"))
                                         : color.R.ToString("X")
                                     ,
                                     color.G.ToString("X").Length == 1
                                         ? String.Format("0{0}", color.G.ToString("X"))
                                         : color.G.ToString("X")
                                     ,
                                     color.B.ToString("X").Length == 1
                                         ? String.Format("0{0}", color.B.ToString("X"))
                                         : color.B.ToString("X")
                                     ,
                                     color.A.ToString("X").Length == 1
                                         ? String.Format("0{0}", color.A.ToString("X"))
                                         : color.A.ToString("X"));
            }

            #endregion Color Conversion
        }

        #endregion

        #region Nested type: Living

        public class Living : Block
        {
            #region Variables

            public float Age
            {
                get { return Variables["Age"].SafeParse<float>(); }
                set { Variables["Age"] = value; }
            }

            #endregion Variables
        }

        #endregion

        #region Nested type: Object

        public class Object : Id
        {
            #region Variables

// ReSharper disable InconsistentNaming
            public float Pos_x
// ReSharper restore InconsistentNaming
            {
                get { return Variables["Pos.x"].SafeParse<float>(); }
                set { Variables["Pos.x"] = value; }
            }

// ReSharper disable InconsistentNaming
            public float Pos_y
// ReSharper restore InconsistentNaming
            {
                get { return Variables["Pos.y"].SafeParse<float>(); }
                set { Variables["Pos.y"] = value; }
            }

            public string Type
            {
                get { return Variables["Type"].SafeParse<string>(); }
                set { Variables["Type"] = value; }
            }

            public int SubType
            {
                get { return Variables["SubType"].SafeParse<int>(); }
                set { Variables["SubType"] = value; }
            }

            #endregion Variables
        }

        #endregion

        #region Nested type: Prisoner

        public class Prisoner : Object
        {
            #region Nested type: Bio

            public class Bio : Human
            {
                #region Variables

                public string Forname
                {
                    get { return Variables["Forname"].SafeParse<string>(); }
                    set { Variables["Forname"] = value; }
                }

                public string Surname
                {
                    get { return Variables["Surname"].SafeParse<string>(); }
                    set { Variables["Surname"] = value; }
                }

                public string Traits
                {
                    get { return Variables["Traits"].SafeParse<string>(); }
                    set { Variables["Traits"] = value; }
                }

                public int Sentence
                {
                    get { return Variables["Sentence"].SafeParse<int>(); }
                    set { Variables["Sentence"] = value; }
                }

                public float Served
                {
                    get { return Variables["Served"].SafeParse<float>(); }
                    set { Variables["Served"] = value; }
                }

                public int Parole
                {
                    get { return Variables["Parole"].SafeParse<int>(); }
                    set { Variables["Parole"] = value; }
                }

                #endregion Variables
            }

            #endregion
        }

        #endregion

        #region Nested type: Visitor

        public class Visitor : Object
        {
            #region Nested type: Bio

            public class Bio : Human
            {
                #region Variables

                public string Type
                {
                    get { return Variables["Type"].SafeParse<string>(); }
                    set { Variables["Type"] = value; }
                }

                public Color ClothingColour
                {
                    get { return HexToColor(Variables["ClothingColour"].SafeParse<string>()); }
                    set { Variables["ClothingColour"] = ColorToHex(value); }
                }

                #endregion Variables
            }

            #endregion
        }

        #endregion
    }
}