using PrisonArchitect.Helper;

namespace PrisonArchitect.PrisonFile.HelperBlocks
{
    public class Cell : HandledBlock
    {
        public int X
        {
            get { return BlockName.Replace("\"", "").Split(null)[0].SafeParse<int>(); }
        }

        public int Y
        {
            get { return BlockName.Replace("\"", "").Split(null)[1].SafeParse<int>(); }
        }
    }
}