using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Moon.Queryable.Tests
{
    public class QueryableExtensionsTests
    {
        private readonly IQueryable<Source> query;
        private readonly Source source;

        public QueryableExtensionsTests()
        {
            source = new Source { FirstName = "First", LastName = "Last" };
            query = new List<Source> { source }.AsQueryable();
        }

        [Fact]
        public void Queryable_Project_To_WithAutomaticProjection_ShouldProjectMatchingProperties()
        {
            var result = query
                .Project().To<Result>()
                .FirstOrDefault();

            Assert.Equal(source.FirstName, result.FirstName);
            Assert.Equal(source.LastName, result.LastName);
            Assert.Null(result.Name);
        }

        [Fact]
        public void Queryable_Project_To_WithCustomProjection_ShouldProjectAllProperties()
        {
            var result = query
                .Project().To<Result, ResultProjection>()
                .FirstOrDefault();

            var expectedName = source.FirstName + " " + source.LastName;

            Assert.Equal(source.FirstName, result.FirstName);
            Assert.Equal(source.LastName, result.LastName);
            Assert.Equal(expectedName, result.Name);
        }

        private class Result
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Name { get; set; }
        }

        private class ResultProjection : Projection<Source, Result>
        {
            public override Expression<Func<Source, Result>> GetExpression()
            {
                return s => new Result
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Name = s.FirstName + " " + s.LastName
                };
            }
        }

        private class Source
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }
    }
}