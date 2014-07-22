using PrisonArchitect.Helper;

namespace PrisonArchitect.PrisonFile.BlockWrappers.Helper
{
    public class Vector2D<T> : HelperBase
    {
        public Vector2D(Block block, string name) : base(block, name) { }

        #region Variables

        // ReSharper disable InconsistentNaming

        public T x { get { return Variables[Name + ".x"].SafeParse<T>(); } set { Variables[Name + ".x"] = value.ToString(); } }

        public T y { get { return Variables[Name + ".y"].SafeParse<T>(); } set { Variables[Name + ".y"] = value.ToString(); } }

        // ReSharper restore InconsistentNaming

        #endregion Variables
    }
}
