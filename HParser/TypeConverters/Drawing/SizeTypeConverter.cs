

namespace HParser.TypeConverters
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Numerics;

    public class SizeTypeConverter : ITypeConverter
    {
        public bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(Size);
        }

        private readonly char[] _separators = { '×', '*', 'x', ' ', '\t' };

        public bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            if (content != null)
            {
                var partials = content.Split(_separators, StringSplitOptions.RemoveEmptyEntries);

                if (partials.Length == 2)
                {
                    var ikt = typeof(int);
                    var g = provider.GetTypeConverter(ikt);
                    if (g.TryConvert(provider, partials.First(), ikt, out var w) &&
                        g.TryConvert(provider, partials.Last(), ikt, out var h))
                    {
                        graph = new Size((int)w, (int)h);
                        return true;
                    }
                }
            }

            graph = null;
            return false;
        }

        public string ToString(ITypeConverterProvider provider, object graph)
        {
            var ikt = typeof(int);
            var g = provider.GetTypeConverter(ikt);
            var sz = ((Size)graph);
            return $"{g.ToString(provider, sz.Width)} x {g.ToString(provider, sz.Height)}";
        }
    }
}
