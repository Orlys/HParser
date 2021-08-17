using System;

namespace HParser.TypeConverters
{
    public class ByteArrayTypeConverter : ITypeConverter
    {
        public bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(byte[]);
        }

        public bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            if (content != null)
            {
                try
                {
                    graph = Convert.FromBase64String(content);
                    return true;
                }
                catch { }
            }
            graph = null;
            return false;
        }

        public string ToString(ITypeConverterProvider provider, object graph)
        {
            return Convert.ToBase64String((byte[])graph);
        }
    }
}
