namespace HParser
{
    using System;
    using System.ComponentModel;

    public class TypeConvertService : ITypeConvertService
    {
        private readonly ITypeConverterProvider _provider;

        public TypeConvertService(ITypeConverterProvider provider)
        {
            this._provider = provider;
        }

        public object ToGraph(Type graphType, string content)
        {
            if (graphType is null)
            {
                throw new ArgumentNullException(nameof(graphType));
            }

            var converter = GetTypeConverter(graphType);
            if (!converter.TryConvert(_provider, content, graphType, out var graph))
            {
                throw new InvalidOperationException("無法轉換的字串格式。");
            }
            return graph;
        }

        private ITypeConverter GetTypeConverter(Type type)
        {
            var typeConverter = this._provider.GetTypeConverter(type);

            if (typeConverter is null)
                throw new NotSupportedException("無可用轉換器");

            return typeConverter;
        }


        public string ToString<TGrpah>(TGrpah graoh)
        {
            var typeKey = typeof(TGrpah);
            var typeConverter = GetTypeConverter(typeKey);

            var content = typeConverter.ToString(_provider, graoh);
            return content;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString()
        {
            return base.ToString();
        }
    }


}
