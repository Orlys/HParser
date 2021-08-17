using NUnit.Framework;
using HParser;

namespace HParser.Tests
{
    using NUnit.Framework;
    using HParser;
    using HParser.TypeConverters;
    using System;
    using System.Collections.Generic;

    [TestFixture()]
    public class TypeConvertServiceTests
    {
        [Test()]
        public void ToGraphTest()
        {
            var provider = new TypeConverterProvider() 
                .Register(new Int32TypeConverter())
                ;

            var service = new TypeConvertService(provider);

            var expected = (int)12;

            var value = service.ToString(expected);

            var actual = service.ToGraph(expected.GetType(), value);

            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void 可為空的結構類型轉換_測試()
        { 
            var provider = new TypeConverterProvider()
                .Register(new NullableTypeConverter())
                .Register(new Int32TypeConverter()) 
                ;


            var service = new TypeConvertService(provider);

            var expected = (int?)12;
            var value = service.ToString(expected);
            var actual = service.ToGraph(expected.GetType(), value);
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void 可為空的列舉類型轉換_測試()
        {
            var provider = new TypeConverterProvider()
                .Register(new NullableTypeConverter())
                .Register(new EnumTypeConverter()) 
                ;

            var service = new TypeConvertService(provider);

            var expected = (DayOfWeek?)DayOfWeek.Friday;
            var value = service.ToString(expected);
            var actual = service.ToGraph(expected.GetType(), value);
            Assert.AreEqual(expected, actual);
        }



        [Test()]
        public void 清單類型轉換_測試()
        {
            var provider = new TypeConverterProvider() 
                .Register(new EnumTypeConverter())
                .Register(new ListTypeConverter())
                ;

            var service = new TypeConvertService(provider);

            var expected = new List<DayOfWeek> { DayOfWeek.Friday };
            var value = service.ToString(expected);
            var actual = service.ToGraph(expected.GetType(), value);

            Assert.IsInstanceOf<List<DayOfWeek>>(actual);

            CollectionAssert.AreEqual(expected, actual as List<DayOfWeek>);
        }


        [Test()]
        public void 可為空的列舉清單類型轉換_測試()
        {
            var provider = new TypeConverterProvider()
                .Register(new NullableTypeConverter())
                .Register(new EnumTypeConverter())
                .Register(new ListTypeConverter())
                ; 
            var service = new TypeConvertService(provider);

            var expected = new List<DayOfWeek?> { DayOfWeek.Friday };
            var value = service.ToString(expected);
            var actual = service.ToGraph(expected.GetType(), value);

            Assert.IsInstanceOf<List<DayOfWeek?>>(actual);

            CollectionAssert.AreEqual(expected, actual as List<DayOfWeek?>);
        }


        [Test()]
        public void 字串清單類型轉換_測試()
        {
            var provider = new TypeConverterProvider()
                .Register(new ListTypeConverter())
                .Register(new StringTypeConverter())
                ;

            var service = new TypeConvertService(provider);

            var expected = new List<string> { "aaa", "bbb" };
            var value = service.ToString(expected);
            var actual = service.ToGraph(expected.GetType(), value);

            Assert.IsInstanceOf<List<string>>(actual);

            CollectionAssert.AreEqual(expected, actual as List<string>);
        }


        [Test()]
        public void 字串轉換_測試()
        {
            var provider = new TypeConverterProvider()
                .Register(new ListTypeConverter())
                .Register(new StringTypeConverter())
                ;

            var service = new TypeConvertService(provider);

            var expected = new List<string> { "'aa   \'a'", "bbb" };
            var value = service.ToString(expected);
            var actual = service.ToGraph<List<string>>(value);

            Assert.IsInstanceOf<List<string>>(actual);

            CollectionAssert.AreEqual(expected, actual  );
        }



        [Test()]
        public void 陣列類型轉換_測試()
        {
            var provider = new TypeConverterProvider()
                .Register(new EnumTypeConverter())
                .Register(new ArrayTypeConverter())
                ; 

            var service = new TypeConvertService(provider);

            var expected = new [] { DayOfWeek.Friday };
            var value = service.ToString(expected);
            var actual = service.ToGraph(expected.GetType(), value);

            Assert.IsInstanceOf<DayOfWeek[]>(actual);

            CollectionAssert.AreEqual(expected, actual as DayOfWeek[]);
        }



        [Test()]
        public void 鍵值對類型轉換_測試()
        {
            var provider = new TypeConverterProvider()
                .Register(new Int32TypeConverter())
                .Register(new KeyValuePairTypeConverter())
                ;

            var service = new TypeConvertService(provider);

            var expected = new KeyValuePair<int, int>(12, 22);
            var value = service.ToString(expected);
            var actual = service.ToGraph<KeyValuePair<int, int>>(value);

            Assert.IsInstanceOf<KeyValuePair<int, int>>(actual);

            Assert.AreEqual(expected, actual);
        }



        [Test()]
        public void 鍵值對清單類型轉換_測試()
        {
            var provider = new TypeConverterProvider()
                .Register(new Int32TypeConverter())
                .Register(new KeyValuePairTypeConverter())
                .Register(new ListTypeConverter())
                .Register(new StringTypeConverter())
                ;

            var service = new TypeConvertService(provider);

            var expected = new List<KeyValuePair<int, string>> {
                new KeyValuePair<int, string>(1, "one"),
                new KeyValuePair<int, string>(2, "tw\\'o"),
            };
            var value = service.ToString(expected);
            var actual = service.ToGraph<List<KeyValuePair<int, string>>>(value);

            Assert.IsInstanceOf<List<KeyValuePair<int, string>>>(actual);

            CollectionAssert.AreEqual(expected, actual);
        }


        [Test()]
        public void 巢狀鍵值對清單類型轉換_測試()
        {
            var provider = new TypeConverterProvider()
                .Register(new Int32TypeConverter())
                .Register(new KeyValuePairTypeConverter())
                .Register(new ListTypeConverter())
                .Register(new StringTypeConverter())
                ;

            var service = new TypeConvertService(provider);

            var expected = new List<KeyValuePair<int, List<string>>> {
                new KeyValuePair<int, List<string>>(1, new List<string>{ "one", "一 壹" }),
                new KeyValuePair<int, List<string>>(2, new List<string>{ "two", "二" }),
            };
            var value = service.ToString(expected);
            var actual = service.ToGraph<List<KeyValuePair<int, List<string>>>>(value);

            Assert.IsInstanceOf<List<KeyValuePair<int, List<string>>>>(actual);

            CollectionAssert.AreEqual(expected, actual);
        }



        [Test()]
        public void 字典類型轉換_測試()
        {
            var provider = new TypeConverterProvider()
                .Register(new Int32TypeConverter())
                .Register(new KeyValuePairTypeConverter())
                .Register(new ListTypeConverter())
                .Register(new StringTypeConverter())
                .Register(new DictionaryTypeConverter())
                ; 
            var service = new TypeConvertService(provider);

            var expected = new Dictionary<int, List<string>>
            {
                [1] = new List<string> { "one", "一 壹" },
                [2] = new List<string> { "two", "二" }, 
            };
            var value = service.ToString(expected);
            var actual = service.ToGraph<Dictionary<int, List<string>>>(value);

            Assert.IsInstanceOf<Dictionary<int, List<string>>>(actual);

            CollectionAssert.AreEqual(expected, actual);
        }
         
    }


}