using System.Collections.Generic;
using Xunit;

namespace Moon.Contracts.Tests
{
    public class RequiresTests
    {
        [Fact]
        public void AssignableTo_TypeIsNotAssignable_ThrowsException()
        {
            var type = typeof(int);

            Assert.Throws<ContractException>(() => Requires.AssignableTo<string>(type, nameof(type)));
        }

        [Fact]
        public void NotEmpty_CollectionIsEmpty_ThrowsException()
        {
            var isEmpty = new List<string>();

            Assert.Throws<ContractException>(() => Requires.NotEmpty(isEmpty, nameof(isEmpty)));
        }

        [Fact]
        public void NotNull_ObjectIsNull_ThrowsException()
        {
            var isNull = (string)null;

            Assert.Throws<ContractException>(() => Requires.NotNull(isNull, nameof(isNull)));
        }

        [Fact]
        public void NotNullOrEmpty_StringIsEmpty_ThrowsException()
        {
            var isEmpty = string.Empty;

            Assert.Throws<ContractException>(() => Requires.NotNullOrEmpty(isEmpty, nameof(isEmpty)));
        }

        [Fact]
        public void NotNullOrWhiteSpace_StringContainsOnlyWhiteSpace_ThrowsException()
        {
            var onlyWhiteSpace = "           ";

            Assert.Throws<ContractException>(() => Requires.NotNullOrWhiteSpace(onlyWhiteSpace, nameof(onlyWhiteSpace)));
        }


        [Fact]
        public void That_ConditionIsFalse_ThrowsException()
        {
            var number = 42;

            Assert.Throws<ContractException>(() => Requires.That(number < 42, "number is not less than 42"));
        }

    }
}