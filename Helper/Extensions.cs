using System;
using System.ComponentModel;

namespace PrisonArchitect.Helper
{
    public static class Extensions
    {
        public static T SafeParse<T>(this object source)
        {
            T val = default(T);
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof (T));
                val = (T) converter.ConvertFrom(source);
            }
            catch (Exception)
            {
            }
            return val;
        }
    }
}