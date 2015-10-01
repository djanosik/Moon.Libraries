using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Moon.OData
{
    /// <summary>
    /// Represents OData query options that can be used to perform query composition. Currently this
    /// only supports $filter, $orderby, $top, $skip, and $count.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity.</typeparam>
    public class ODataQuery<TEntity>
    {
        readonly Lazy<bool?> count;
        readonly Lazy<FilterClause> filter;
        readonly Lazy<OrderByClause> orderBy;
        readonly Lazy<long?> skip;
        readonly Lazy<long?> top;

        /// <summary>
        /// Initializes a new instance of the <see cref="ODataQuery{TEntity}" /> class.
        /// </summary>
        /// <param name="queryOptions">The dictionary storing query option key-value pairs.</param>
        public ODataQuery(IDictionary<string, string> queryOptions)
            : this(queryOptions, Enumerable.Empty<IPrimitiveType>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ODataQuery{TEntity}" /> class.
        /// </summary>
        /// <param name="queryOptions">The dictionary storing query option key-value pairs.</param>
        /// <param name="primitives">An enumeration of additional primitive types.</param>
        public ODataQuery(IDictionary<string, string> queryOptions, IEnumerable<IPrimitiveType> primitives)
        {
            Requires.NotNull(queryOptions, nameof(queryOptions));
            Requires.NotNull(primitives, nameof(primitives));

            var model = GetEdmModel(GetPrimitives(primitives).ToDictionary(p => p.Type));
            var entities = model.FindDeclaredNavigationSource("Entities");

            var parser = new ODataQueryOptionParser(model, entities.EntityType(),
                entities, queryOptions);

            count = Lazy.From(parser.ParseCount);
            filter = Lazy.From(parser.ParseFilter);
            orderBy = Lazy.From(parser.ParseOrderBy);
            skip = Lazy.From(parser.ParseSkip);
            top = Lazy.From(parser.ParseTop);
        }

        /// <summary>
        /// Gets a parsed $count query option.
        /// </summary>
        public bool? Count
            => count.Value;

        /// <summary>
        /// Gets a $filter clause parsed into semantic nodes.
        /// </summary>
        public FilterClause Filter
            => filter.Value;

        /// <summary>
        /// Gets a $oderby clause parsed into semantic nodes.
        /// </summary>
        public OrderByClause OrderBy
            => orderBy.Value;

        /// <summary>
        /// Gets a parsed $skip query option.
        /// </summary>
        public long? Skip
            => skip.Value;

        /// <summary>
        /// Gets a parsed $top query option.
        /// </summary>
        public long? Top
            => top.Value;

        static IEnumerable<IPrimitiveType> GetPrimitives(IEnumerable<IPrimitiveType> primitives)
        {
            yield return new PrimitiveType<byte[]>(EdmPrimitiveTypeKind.Binary);
            yield return new PrimitiveType<bool>(EdmPrimitiveTypeKind.Boolean);
            yield return new PrimitiveType<byte>(EdmPrimitiveTypeKind.Byte);
            yield return new PrimitiveType<DateTime>(EdmPrimitiveTypeKind.Date);
            yield return new PrimitiveType<DateTimeOffset>(EdmPrimitiveTypeKind.DateTimeOffset);
            yield return new PrimitiveType<decimal>(EdmPrimitiveTypeKind.Decimal);
            yield return new PrimitiveType<double>(EdmPrimitiveTypeKind.Double);
            yield return new PrimitiveType<Guid>(EdmPrimitiveTypeKind.Guid);
            yield return new PrimitiveType<short>(EdmPrimitiveTypeKind.Int16);
            yield return new PrimitiveType<int>(EdmPrimitiveTypeKind.Int32);
            yield return new PrimitiveType<long>(EdmPrimitiveTypeKind.Int64);
            yield return new PrimitiveType<sbyte>(EdmPrimitiveTypeKind.SByte);
            yield return new PrimitiveType<float>(EdmPrimitiveTypeKind.Single);
            yield return new PrimitiveType<string>(EdmPrimitiveTypeKind.String);
            yield return new PrimitiveType<char>(EdmPrimitiveTypeKind.String);
            yield return new PrimitiveType<char[]>(EdmPrimitiveTypeKind.String);
            yield return new PrimitiveType<Type>(EdmPrimitiveTypeKind.String);
            yield return new PrimitiveType<Uri>(EdmPrimitiveTypeKind.String);
            yield return new PrimitiveType<TimeSpan>(EdmPrimitiveTypeKind.Duration);

            foreach (var primitive in primitives)
            {
                yield return primitive;
            }
        }

        IEdmModel GetEdmModel(IDictionary<Type, IPrimitiveType> primitives)
        {
            var model = new EdmModel();
            var container = new EdmEntityContainer("Default", "Container");
            container.AddEntitySet("Entities", GetEdmType(typeof(TEntity), primitives));
            model.AddElement(container);

            return model;
        }

        IEdmEntityType GetEdmType(Type type, IDictionary<Type, IPrimitiveType> primitives)
        {
            var entityType = new EdmEntityType(type.Namespace, type.Name);

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (primitives.ContainsKey(property.PropertyType))
                {
                    var primitive = primitives[property.PropertyType];
                    entityType.AddKeys(entityType.AddStructuralProperty(property.Name, primitive.Kind));
                }
                else
                {
                    var propertyType = GetEdmType(property.PropertyType, primitives);
                    var reference = new EdmEntityTypeReference(propertyType, IsNullable(property.PropertyType));
                    entityType.AddKeys(entityType.AddStructuralProperty(property.Name, reference));
                }
            }

            return entityType;
        }

        bool IsNullable(Type type)
            => !type.GetTypeInfo().IsValueType || Nullable.GetUnderlyingType(type) != null;
    }
}