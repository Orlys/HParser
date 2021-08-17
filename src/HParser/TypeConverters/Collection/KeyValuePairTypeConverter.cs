using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HParser.TypeConverters
{
    //public class KeyValuePairTypeConverter : ITypeConverter
    //{
    //    public bool CanConvert(ITypeConverterProvider provider, Type t)
    //    {
    //        if (!t.IsGenericType)
    //            return false;

    //        return t.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);
    //    }

    //    public bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
    //    {
    //        var targ = graphType.GetGenericArguments();

    //    }

    //    public string ToString(ITypeConverterProvider provider, object graph)
    //    {
    //        return
    //    }
    //}

    public class KeyValuePairTypeConverter : ITypeConverter
    {
        public bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            if (!t.IsGenericType)
                return false;

            return t.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);
        }

        public bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            graph = null;
            if (content is null)
            {
                return false;
            }

            var index = 0;
            var anchorIndex = 0;
            while ((index = content.IndexOf("=", index)) != -1)
            {
                var beforeSoe = content.ElementAtOrDefault(index - 1);
                if (beforeSoe == '\\')
                {
                    index++;
                    continue;
                }
                anchorIndex = index;
                break;
            }

            var targ = graphType.GetGenericArguments();

            var keyPart = content.Substring(0, anchorIndex).Replace("\\=", "=");
            var keyType = targ.First();

            var ktc = provider.GetTypeConverter(keyType);
            if (!ktc.TryConvert(provider, keyPart, keyType, out var key))
            {
                return false;
            }

            var valuePart = content.Substring(anchorIndex + 1).Replace("\\=", "="); 
            var valueType = targ.Last();

            var vtc = provider.GetTypeConverter(valueType);
            if (!vtc.TryConvert(provider, valuePart, valueType, out var value))
            {
                return false;
            }

            graph = Activator.CreateInstance(graphType, new object[] { key, value });
            return true;
        }

        public string ToString(ITypeConverterProvider provider, object graph)
        {
            var t = graph.GetType();
            var keyStr = GetFieldValueString(provider, graph, t, "key");
            var valueStr = GetFieldValueString(provider, graph, t, "value");

            return $"{keyStr}={valueStr}";
        }

        private string GetFieldValueString(ITypeConverterProvider provider, object graph, Type t, string fieldName)
        {
            var value = t.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var vtc = provider.GetTypeConverter(value.FieldType);
            var valueStr = vtc.ToString(provider, value.GetValue(graph))?.Replace("=", "\\=");
            return valueStr;
        }
    }

}
