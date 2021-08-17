namespace HParser
{
    public static class ITypeConverterProviderExtensions
    {
        public static ITypeConverterProvider Register<TConverter>(this ITypeConverterProvider provider) where TConverter : ITypeConverter, new()
        {
            return provider.Register(new TConverter());
        }
    }
}
