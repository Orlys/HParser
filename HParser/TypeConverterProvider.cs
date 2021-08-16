namespace HParser
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public class TypeConverterProvider : ITypeConverterProvider
    {
        public TypeConverterProvider() : this(null)
        { 
        }

        public TypeConverterProvider(IEnumerable<ITypeConverter> converters)
        {
            this._typeConverters = converters?.ToList() ?? new List<ITypeConverter>();
            this._cache = new ConcurrentDictionary<Type, ITypeConverter>();
        }

        public ITypeConverterProvider Register(ITypeConverter converter)
        {
            if (converter is null)
                throw new ArgumentNullException(nameof(converter));

            this._typeConverters.Add(converter);
            return this;
        }

        private readonly ConcurrentDictionary<Type, ITypeConverter> _cache;
        private readonly List<ITypeConverter> _typeConverters;

        public ITypeConverter GetTypeConverter(Type type)
        {
            if(!_cache.TryGetValue(type, out var tc))
            {
                tc = _typeConverters.FirstOrDefault(x => x.CanConvert(this, type));
                if(tc is null)
                {
                    return null;
                }

                _cache.TryAdd(type, tc);
            }

            return tc;
             
        }
         
    }


}
