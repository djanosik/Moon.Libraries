using FluentAssertions;
using Xbehave;

namespace Moon.Security.Tests
{
    public class PasswordTests
    {
        int length;
        string result;

        [Scenario]
        public void GeneratingPassword()
        {
            "Given the length"
                .x(() => length = 10);

            "When I generate a password"
                .x(() => result = Password.Generate(length));

            "Then it should be 10 chars long"
                .x(() =>
                {
                    result.Should().HaveLength(length);
                });
        }
    }
}