using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }
    }
}