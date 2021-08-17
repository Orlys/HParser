using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HParser
{

    internal static class DictionaryHelper
    {
        public static MethodInfo AdaptMethod = typeof(DictionaryHelper).GetMethod("Adapt", BindingFlags.NonPublic | BindingFlags.Static);

        private static Dictionary<TKey, TValue> Adapt<TKey, TValue>(List<KeyValuePair<TKey, TValue>> list)
        {
            return list.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
