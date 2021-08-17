namespace HParser
{
    using HParser.TypeConverters;

    using System;

    using UriTypeConverter = TypeConverters.UriTypeConverter;

    public static partial class ServiceLocator
    {
        static partial void RegisterBuiltinConverter(ITypeConverterProvider provider);

        public static ITypeConverterProvider Provider => s_provider.Value;

        public static ITypeConvertService Service => s_service.Value;

        private readonly static Lazy<ITypeConverterProvider> s_provider;
        private readonly static Lazy<ITypeConvertService> s_service;

        static ServiceLocator()
        {
            s_provider = new Lazy<ITypeConverterProvider>(() =>
            {
                var p = new TypeConverterProvider()
                    .Register<ArrayTypeConverter>()
                    .Register<ByteArrayTypeConverter>()
                    .Register<DictionaryTypeConverter>()
                    .Register<KeyValuePairTypeConverter>()
                    .Register<ListTypeConverter>()
                    .Register<ColorTypeConverter>()
                    .Register<PointFTypeConverter>()
                    .Register<PointTypeConverter>()
                    .Register<SizeFTypeConverter>()
                    .Register<SizeTypeConverter>()
                    .Register<DnsEndPointTypeConverter>()
                    .Register<IPAddressTypeConverter>()
                    .Register<IPEndPointTypeConverter>()
                    .Register<NetworkCredentialTypeConverter>()
                    .Register<UriTypeConverter>()
                    .Register<EnumTypeConverter>()
                    .Register<NullableTypeConverter>()
                    .Register<StringTypeConverter>();
                RegisterBuiltinConverter(p);
                return p;
            });
            s_service = new Lazy<ITypeConvertService>(() => new TypeConvertService(Provider));
        }
    }
}
