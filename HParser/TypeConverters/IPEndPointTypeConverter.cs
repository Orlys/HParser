using System;
using System.Net;

namespace HParser.TypeConverters
{
    public class IPEndPointTypeConverter : ITypeConverter
    {

        public bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(IPEndPoint);
        }

        public bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {

            if (content is null)
            {
                graph = null;
                return false;
            }


            string addressPart = null;
            string portPart = null;
            graph = null;

            if (string.IsNullOrWhiteSpace(content))
            {
                return false;
            }


            var lastColonIndex = content.LastIndexOf(':');
            if (lastColonIndex > 0)
            {
                // IPv4 with port or IPv6
                var closingIndex = content.LastIndexOf(']');
                if (closingIndex > 0)
                {
                    // IPv6 with brackets
                    addressPart = content.Substring(1, closingIndex - 1);
                    if (closingIndex < lastColonIndex)
                    {
                        // IPv6 with port [::1]:80
                        portPart = content.Substring(lastColonIndex + 1);
                    }
                }
                else
                {
                    // IPv6 without port or IPv4
                    var firstColonIndex = content.IndexOf(':');
                    if (firstColonIndex != lastColonIndex)
                    {
                        // IPv6 ::1
                        addressPart = content;
                    }
                    else
                    {
                        // IPv4 with port 127.0.0.1:123
                        addressPart = content.Substring(0, firstColonIndex);
                        portPart = content.Substring(firstColonIndex + 1);
                    }
                }
            }
            else
            {
                // IPv4 without port
                addressPart = content;
            }

            var addressTypeKey = typeof(IPAddress);
            var ipAddressConverter = provider.GetTypeConverter(addressTypeKey);

            if (ipAddressConverter.TryConvert(provider, addressPart, addressTypeKey, out var address))
            {
                if (portPart != null)
                {
                    var i32TypeKey = typeof(int);
                    var i32Converter = provider.GetTypeConverter(i32TypeKey);

                    if (i32Converter.TryConvert(provider, portPart, i32TypeKey, out var port))
                    {
                        graph = new IPEndPoint((IPAddress)address, (int)port);
                        return true;
                    }

                    return false;
                }
                graph = new IPEndPoint((IPAddress)address, 0);
                return true;
            }
            return false;


        }

        public string ToString(ITypeConverterProvider provider, object graph)
        {
            var de = (IPEndPoint)graph;
             
            var iec = provider.GetTypeConverter(typeof(IPAddress));
            var u16c = provider.GetTypeConverter(typeof(ushort));

            return $"{iec.ToString(provider, de.Address)}:{u16c.ToString(provider, de.Port)}";
        }
    }
}
