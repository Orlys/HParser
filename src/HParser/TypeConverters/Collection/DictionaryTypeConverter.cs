using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HParser.TypeConverters
{
    public class DictionaryTypeConverter : ITypeConverter
    {
        public bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            if (!t.IsGenericType)
                return false;
            return typeof(Dictionary<,>).IsAssignableFrom(t.GetGenericTypeDefinition());
        }

        public bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var g = graphType.GetGenericArguments();
            var keyType = g.First();
            var valueType = g.Last();

            var kvpListTypeKey = typeof(List<>).MakeGenericType(typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType));
            var gx = provider.GetTypeConverter(kvpListTypeKey);


            if (gx.TryConvert(provider, content, kvpListTypeKey, out var kvpList))
            {
                graph = DictionaryHelper.AdaptMethod.MakeGenericMethod(keyType, valueType).Invoke(null, new[] { kvpList });
                return true;
            }

            graph = null;
            return false;
        }

        public string ToString(ITypeConverterProvider provider, object graph)
        {
            var g = graph.GetType().GetGenericArguments();
            var keyType = g.First();
            var valueType = g.Last();

            var kvpListTypeKey = typeof(List<>).MakeGenericType(typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType));
            var gx = provider.GetTypeConverter(kvpListTypeKey);

            var v = Activator.CreateInstance(kvpListTypeKey, new object[] { graph });
            var str = gx.ToString(provider, v);
             
            return str;
        }
    }
}
