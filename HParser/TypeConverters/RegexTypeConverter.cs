using System;
using System.Collections;
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
            if(content is null)
            {
                graph = null;
                return false;
            }

            try
            {
                // todo: snapshot or cache
                graph = new Regex(content);
                return true;
            }
            catch
            {
                graph = null;
                return false;
            } 
        }

        public string ToString(ITypeConverterProvider provider, object graph)
        {
            return ((Regex)graph).ToString();
        }
    }
}
