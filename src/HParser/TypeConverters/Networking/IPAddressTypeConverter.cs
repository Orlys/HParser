using System;
using System.Net;

namespace HParser.TypeConverters
{
    public class IPAddressTypeConverter : ITypeConverter
    {
        public bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(IPAddress);
        }

        public bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            if (content != null)
            {
                var flag = IPAddress.TryParse(content, out var v);
                graph = v;
                return flag;
            }

            graph = null;
            return false;
        }

        public string ToString(ITypeConverterProvider provider, object graph)
        {
            return (graph as IPAddress)?.ToString();
        }
    }
}
