using System.Collections.Generic;
using FluentAssertions;
using Moon.Testing;
using Xunit;

namespace Moon.Reflection.Tests
{
    public class AnonymousTests : TestSetup
    {
        object anonymous;
        IDictionary<string, string> result;

        [Fact]
        public void ConvertingAnonymousToDictionary()
        {
            "Given the anonymous object"
                .x(() => anonymous = new { test = "test", test2 = "test2" });

            "When I convert the object to dictionary"
                .x(() => result = Anonymous.ToDictionary(anonymous));

            "Then it should have two items"
                .x(() =>
                {
                    result["test"].Should().Be("test");
                    result["test2"].Should().Be("test2");
                });
        }
    }
}