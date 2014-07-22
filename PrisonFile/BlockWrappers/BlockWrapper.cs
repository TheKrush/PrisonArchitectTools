using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PrisonArchitect.Helper;

namespace PrisonArchitect.PrisonFile.BlockWrappers
{
    public class BlockWrapper
    {
        public Block Block;

        public BlockWrapper(Block block) { Block = block; }

        public SafeDictionary<string, object> Variables { get { return Block.Variables; } }

        public List<Block> Blocks { get { return Block.Blocks; } }

        public Dictionary<string, object> GetUnhandledVariables()
        {
            Dictionary<string, object> output = new Dictionary<string, object>();

            List<string> unhandled = Variables.Keys.Where(key => !key.Contains(".") && !GetType().GetProperties().Select(prop => prop.Name).Contains(key)).ToList();
            foreach (string key in unhandled)
                output[key] = Variables[key];

            List<string> keys = Variables.Keys.Where(key => key.Contains(".")).ToList();
            foreach (string key in keys)
            {
                string[] keySplit = key.Split('.');
                PropertyInfo propertyInfo = GetType().GetProperty(keySplit[0]);
                if (propertyInfo != null) propertyInfo = propertyInfo.PropertyType.GetProperty(keySplit[1]);
                if (propertyInfo == null) output[key] = Variables[key];
            }

            return output;
        }

        public override string ToString()
        {
            return Block == null ? "" : Block.ToString();
        }
    }
}
