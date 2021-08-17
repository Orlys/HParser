

namespace HParser.TypeConverters
{
    using System;
    using System.Drawing;
    using System.Linq;

    public class PointFTypeConverter : ITypeConverter
    {
        public bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(PointF);
        }

        private readonly char[] _separators = { '×', '*', 'x', ' ', '\t' };

        public bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            if (content != null)
            {
                var partials = content.Split(_separators, StringSplitOptions.RemoveEmptyEntries);

                if (partials.Length == 2)
                {
                    var ikt = typeof(float);
                    var g = provider.GetTypeConverter(ikt);
                    if (g.TryConvert(provider, partials.First(), ikt, out var x) &&
                        g.TryConvert(provider, partials.Last(), ikt, out var y))
                    {
                        graph = new PointF((float)x, (float)y);
                        return true;
                    }
                }
            }

            graph = null;
            return false;
        }

        public string ToString(ITypeConverterProvider provider, object graph)
        {
            var ikt = typeof(float);
            var g = provider.GetTypeConverter(ikt);
            var sz = ((PointF)graph);
            return $"{g.ToString(provider, sz.X)} x {g.ToString(provider, sz.Y)}";
        }
    }
}
