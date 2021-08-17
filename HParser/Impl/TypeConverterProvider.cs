namespace HParser
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public class TypeConverterProvider : ITypeConverterProvider
    {
        public TypeConverterProvider()
        {
            this._typeConverters = new List<ITypeConverter>();
            this._cache = new ConcurrentDictionary<Type, ITypeConverter>();
        }

        public ITypeConverterProvider Register(ITypeConverter converter)
        {
            if (converter is null)
                throw new ArgumentNullException(nameof(converter));
            lock (_lock)
            {
                this._typeConverters.Add(converter);
            }
            return this;
        }


        public ITypeConverterProvider Deregister(ITypeConverter converter)
        {
            if (converter is null)
                throw new ArgumentNullException(nameof(converter));

            lock (_lock)
            {
                this._typeConverters.Remove(converter);
            }
            return this;
        }

        public IReadOnlyList<ITypeConverter> TypeConverters => _typeConverters.AsReadOnly();

        private readonly ConcurrentDictionary<Type, ITypeConverter> _cache;
        private readonly List<ITypeConverter> _typeConverters;
        private readonly object _lock = new object();

        public ITypeConverter GetTypeConverter(Type type)
        {
            if (!_cache.TryGetValue(type, out var tc))
            {
                lock (_lock)
                {
                    tc = _typeConverters.FirstOrDefault(x => x.CanConvert(this, type));
                    if (tc is null)
                    {
                        return null;
                    }
                }

                _cache.TryAdd(type, tc);
            }

            return tc;

        }

    }


}
