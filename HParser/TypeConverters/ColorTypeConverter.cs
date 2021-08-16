using System;
using System.Drawing;

namespace HParser.TypeConverters
{
    public class ColorTypeConverter : ITypeConverter
    {
        public bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(Color);
        }


        public bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            if (content != null)
            {
                try
                {
                    graph = ColorTranslator.FromHtml(content);
                    return true;
                }
                catch
                {
                }
            }

            graph = null;
            return false;
        }

        public string ToString(ITypeConverterProvider provider, object graph)
        {
            return ColorTranslator.ToHtml((Color)graph);
        }


    }
}
