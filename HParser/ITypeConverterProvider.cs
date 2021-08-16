namespace HParser
{
    using System;

    public interface ITypeConverterProvider
    {
        ITypeConverter GetTypeConverter(Type type);
        ITypeConverterProvider Register(ITypeConverter converter);
    }


}
