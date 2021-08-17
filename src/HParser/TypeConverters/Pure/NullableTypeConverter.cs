namespace HParser.TypeConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NullableTypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            var structType = Nullable.GetUnderlyingType(t);
            if (structType is null)
                return false;
            return provider.GetTypeConverter(structType) != null;
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        { 
            if (content is null)
            {
                graph = null;
                return true;
            }
             
            var structType = Nullable.GetUnderlyingType(graphType);
            var converter = provider.GetTypeConverter(structType);

            var flag = converter.TryConvert(provider, content, structType, out graph);
            return flag;
        }

        public string ToString(ITypeConverterProvider provider, object graph)
        {
            if (graph is null)
                return null;

            var structType = graph.GetType(); 
            var converter = provider.GetTypeConverter(structType);
            
            return converter.ToString(provider, graph);
        }
    }
}
