using System.Collections.Generic;
using Xunit;

namespace Moon.Contracts.Tests
{
    public class RequiresTests
    {
        [Fact]
        public void RequiringAssignableTo()
        {
            var type = typeof(int);

            Assert.Throws<ContractException>(() => Requires.AssignableTo<string>(type, nameof(type)));
        }

        [Fact]
        public void RequiringCustomCondition()
        {
            var number = 42;

            Assert.Throws<ContractException>(() => Requires.That(number < 42, "number is not less than 42"));
        }

        [Fact]
        public void RequiringNotEmptyCollection()
        {
            var isEmpty = new List<string>();

            Assert.Throws<ContractException>(() => Requires.NotEmpty(isEmpty, nameof(isEmpty)));
        }

        [Fact]
        public void RequiringNotNullAndNotEmptyString()
        {
            var isEmpty = string.Empty;

            Assert.Throws<ContractException>(() => Requires.NotNullOrEmpty(isEmpty, nameof(isEmpty)));
        }

        [Fact]
        public void RequiringNotNullAndNotWhiteSpaceString()
        {
            var onlyWhiteSpace = "           ";

            Assert.Throws<ContractException>(() => Requires.NotNullOrWhiteSpace(onlyWhiteSpace, nameof(onlyWhiteSpace)));
        }

        [Fact]
        public void RequiringNotNullObject()
        {
            var isNull = (string)null;

            Assert.Throws<ContractException>(() => Requires.NotNull(isNull, nameof(isNull)));
        }
    }
}