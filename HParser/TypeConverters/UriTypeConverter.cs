namespace HParser.TypeConverters
{
    using System;

    public class UriTypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(Uri);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            graph = new Uri(content);
            return true;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }
}
