using System;
using System.Drawing;

namespace HParser.TypeConverters
{
    public class SizeTypeConverter : ITypeConverter
    {
        public bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(Size);
        }


        public bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            if (content != null)
            {
                var partials = content.Split(new[] { '×', '*', 'x', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (partials.Length == 2 && int.TryParse(partials[0], out var w) &&
                    int.TryParse(partials[1], out var h))
                {
                    graph = new Size(w, h);
                    return true;
                }
            }

            graph = null;
            return false;
        }

        public string ToString(ITypeConverterProvider provider, object graph)
        {
            var sz = ((Size)graph);
            return $"{sz.Width}x{sz.Height}";
        } 
    }
}
