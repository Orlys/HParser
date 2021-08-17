using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HParser.TypeConverters
{
    public class RegexTypeConverter : ITypeConverter
    {
        public bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(Regex);
        }



        public bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var separator = content.IndexOf(':');
            if(separator == -1)
            {
                graph = null;
                return false;
            }


            var optType = typeof(RegexOptions);
            var g = provider.GetTypeConverter(optType);

            if (!g.TryConvert(provider, content.Substring(0, separator), optType, out var f))
            {
                graph = null;
                return false;
            }


            var flag = RegexHelper.Test(content.Substring(separator + 1), (RegexOptions)f, out var r);
            graph = r;
            return flag;
        }

        public string ToString(ITypeConverterProvider provider, object graph)
        {
            var r = (Regex)graph;

            var optType = typeof(RegexOptions);
            var g = provider.GetTypeConverter(optType);
            return $"{g.ToString(provider, r.Options)}:{r.ToString()}";
        }
    }

}
