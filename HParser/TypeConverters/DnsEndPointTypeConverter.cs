using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;

namespace HParser.TypeConverters
{


    public class DnsEndPointTypeConverter : ITypeConverter
    {

        public bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(DnsEndPoint);
        }

        public bool TryConvert(ITypeConverterProvider provider, string domainOrAddressWithPort, Type graphType, out object graph)
        {
            graph = null;
            if (string.IsNullOrWhiteSpace(domainOrAddressWithPort))
                return false;

            var ipeTypeKey = typeof(IPEndPoint);
            var ipec = provider.GetTypeConverter(ipeTypeKey);

            if (ipec.TryConvert(provider,domainOrAddressWithPort, ipeTypeKey, out var ipe))
            {
                var ipEndpoint = (IPEndPoint)ipe;
                graph = new DnsEndPoint(ipEndpoint.Address.ToString(), ipEndpoint.Port);
                return true;
            }

            ushort port = 0;
            var parts = domainOrAddressWithPort.Split(":".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
            switch (parts.Length)
            {
                case 2:
                    {
                        var u16TypeKey = typeof(ushort);
                        var u16converter = provider.GetTypeConverter(u16TypeKey);
                        if (!u16converter.TryConvert(provider,parts.Last(), u16TypeKey, out var p))
                        {
                            goto default;
                        }
                        port = (ushort)p;
                        goto case 1;
                    }

                case 1:
                    {
                        var flagAndDomainOrAdrress = parts.First();
                        if (DomainValidator.Validate(flagAndDomainOrAdrress))
                        {
                            graph = new DnsEndPoint(flagAndDomainOrAdrress, port);
                            return true;
                        }

                        goto default;
                    }

                default:
                    return false;
            }
        }

        public string ToString(ITypeConverterProvider provider, object graph)
        {
            var de = (DnsEndPoint)graph;
            var u16c = provider.GetTypeConverter(typeof(ushort));

            return $"{de.Host}:{u16c.ToString(provider, de.Port)}";

        }  
    }

}
