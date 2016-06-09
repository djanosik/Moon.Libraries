﻿using FluentAssertions;
using Xbehave;

namespace Moon.Tests
{
    public class MimeTypeTests
    {
        string input, result;

        [Scenario]
        public void GettingTypeOfKnownFileName()
        {
            "Given the file name"
                .x(() => input = "data.json");

            "When I get MIME type for the file name"
                .x(() => result = MimeType.Get(input));

            "Then it should be 'application/json'"
                .x(() =>
                {
                    result.Should().Be("application/json");
                });
        }

        [Scenario]
        public void GettingTypeOfKnownExtension()
        {
            "Given the file extension"
                .x(() => input = ".json");

            "When I get MIME type for the file extension"
                .x(() => result = MimeType.Get(input));

            "Then it should be 'application/json'"
                .x(() =>
                {
                    result.Should().Be("application/json");
                });
        }

        [Scenario]
        public void GettingTypeOfUnknownExtension()
        {
            "Given the file extension"
                .x(() => input = ".unk");

            "When I get MIME type for the file extension"
                .x(() => result = MimeType.Get(input));

            "Then it should be 'application/octet-stream'"
                .x(() =>
                {
                    result.Should().Be("application/octet-stream");
                });
        }
    }
}