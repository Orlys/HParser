using NUnit.Framework;
using HParser;
using Moq;
using System;
using System.Collections.Generic;

namespace HParser.Tests
{
    [TestFixture()]
    public class TypeConverterProviderTests
    {
        [Test()]
        public void GetTypeConverterTest()
        {
            var typeKey = typeof(Unit);
            var provider = new TypeConverterProvider();

            var unitConverter = new Mock<ITypeConverter>();
            unitConverter
                .Setup(x => x.CanConvert(provider, typeKey))
                .Returns(true);

            provider.Register(unitConverter.Object);


            Assert.IsInstanceOf<ITypeConverter>(provider.GetTypeConverter(typeKey));

        }


        [Test()]
        public void GetTypeConverterTest2()
        {
            var typeKey = typeof(Unit);

            var provider = new TypeConverterProvider();

            Assert.IsNull(provider.GetTypeConverter(typeKey)); 
        }
    }

    public struct Unit
    {
    }
}