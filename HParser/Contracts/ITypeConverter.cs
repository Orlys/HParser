namespace HParser
{
    using System;
    using System.Linq;

    public interface ITypeConverter
    {
        bool CanConvert(ITypeConverterProvider provider, Type t);

        bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph);

        string ToString(ITypeConverterProvider provider, object graph);
    }

}
