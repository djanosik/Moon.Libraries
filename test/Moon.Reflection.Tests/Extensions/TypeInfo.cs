using System.Reflection;
using Xunit;

namespace Moon.Reflection.Tests
{
    public class TypeInfoExtensionsTests
    {
        [Fact]
        public void Implements_GenericInterface_ShouldReturnFalse()
        {
            var typeInfo = typeof(Test).GetTypeInfo();

            Assert.False(typeInfo.Implements(typeof(ITest<int>).GetTypeInfo()));
        }

        [Fact]
        public void Implements_GenericInterface_ShouldReturnTrue()
        {
            var typeInfo = typeof(Test).GetTypeInfo();

            Assert.True(typeInfo.Implements(typeof(ITest<string>).GetTypeInfo()));
        }

        [Fact]
        public void Implements_GenericInterfaceTypeDefinition_ShouldReturnTrue()
        {
            var typeInfo = typeof(Test).GetTypeInfo();

            Assert.True(typeInfo.Implements(typeof(ITest<>).GetTypeInfo()));
        }

        [Fact]
        public void Implements_NonGenericInterface_ShouldReturnTrue()
        {
            var typeInfo = typeof(Test).GetTypeInfo();

            Assert.True(typeInfo.Implements(typeof(ITest).GetTypeInfo()));
        }

        public interface ITest
        {
        }

        public interface ITest<T>
        {
        }

        public class Test : ITest<string>, ITest
        {
        }
    }
}