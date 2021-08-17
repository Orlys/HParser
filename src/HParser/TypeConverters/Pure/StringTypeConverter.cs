using System;

namespace HParser.TypeConverters
{


    public class StringTypeConverter : ITypeConverter
    {
        private const string Quote = "'";

        public bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(string);
        }

        public bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            graph = null;
            if (content is null)
                return true;

            if(content.StartsWith(Quote) && content.EndsWith(Quote))
            {
                content = content.Remove(content.Length - 1, 1).Remove(0, 1);
            }

            graph = content.Replace("\\" + Quote, Quote);
            return true;
        }

        public string ToString(ITypeConverterProvider provider, object graph)
        {
            var g = ((string)graph)?.Replace(Quote, "\\" + Quote);
            return g;
        }
    }
}
