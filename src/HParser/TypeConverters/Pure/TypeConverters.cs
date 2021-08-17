 
namespace HParser
{
    using HParser.TypeConverters;
    static partial class ServiceLocator
    {
        static partial void RegisterBuiltinConverter(ITypeConverterProvider provider)
        {
            provider.Register<BooleanTypeConverter>();
            provider.Register<CharTypeConverter>();
            provider.Register<ByteTypeConverter>();
            provider.Register<SByteTypeConverter>();
            provider.Register<Int16TypeConverter>();
            provider.Register<UInt16TypeConverter>();
            provider.Register<Int32TypeConverter>();
            provider.Register<UInt32TypeConverter>();
            provider.Register<Int64TypeConverter>();
            provider.Register<UInt64TypeConverter>();
            provider.Register<SingleTypeConverter>();
            provider.Register<DoubleTypeConverter>();
            provider.Register<GuidTypeConverter>();
            provider.Register<DecimalTypeConverter>();
            provider.Register<DateTimeTypeConverter>();
            provider.Register<DateTimeOffsetTypeConverter>();
            provider.Register<TimeSpanTypeConverter>();
            provider.Register<BigIntegerTypeConverter>();
            provider.Register<VersionTypeConverter>();
        }
    }
}

namespace HParser.TypeConverters
{
    using System; 
    using System.Numerics; 

    
    public class BooleanTypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(Boolean);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = Boolean.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

    
    public class CharTypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(Char);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = Char.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

    
    public class ByteTypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(Byte);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = Byte.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

    
    public class SByteTypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(SByte);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = SByte.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

    
    public class Int16TypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(Int16);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = Int16.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

    
    public class UInt16TypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(UInt16);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = UInt16.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

    
    public class Int32TypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(Int32);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = Int32.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

    
    public class UInt32TypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(UInt32);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = UInt32.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

    
    public class Int64TypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(Int64);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = Int64.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

    
    public class UInt64TypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(UInt64);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = UInt64.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

    
    public class SingleTypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(Single);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = Single.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

    
    public class DoubleTypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(Double);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = Double.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

    
    public class GuidTypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(Guid);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = Guid.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

    
    public class DecimalTypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(Decimal);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = Decimal.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

    
    public class DateTimeTypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(DateTime);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = DateTime.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

    
    public class DateTimeOffsetTypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(DateTimeOffset);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = DateTimeOffset.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

    
    public class TimeSpanTypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(TimeSpan);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = TimeSpan.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

    
    public class BigIntegerTypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(BigInteger);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = BigInteger.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

    
    public class VersionTypeConverter : ITypeConverter
    {
        public virtual bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(Version);
        }

        public virtual bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            var flag = Version.TryParse(content, out var value);
            graph = value;
            return flag;
        }

        public virtual string ToString(ITypeConverterProvider provider, object graph)
        {
            return graph.ToString();
        }
    }

 
}
