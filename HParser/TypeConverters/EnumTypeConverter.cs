using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace HParser.TypeConverters
{
    public class EnumTypeConverter : ITypeConverter
    {
        public bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t.IsEnum;
        }

        public bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            try
            {
                graph = Enum.Parse(graphType, content);
                return true;
            }
            catch
            {
                graph = Activator.CreateInstance(graphType);
                return false;
            }
        }

        public string ToString(ITypeConverterProvider provider, object graph)
        {
            return ((Enum)graph).ToString("G");
        }
    }
}
