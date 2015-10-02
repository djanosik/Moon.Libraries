using System.Collections.Generic;

namespace Moon.OData
{
    /// <summary>
    /// This class describes the validation settings for querying.
    /// </summary>
    public class ODataValidationSettings
    {
        /// <summary>
        /// Gets or sets a list of allowed arithmetic operators in the $filter query.
        /// </summary>
        public AllowedArithmeticOperators AllowedArithmeticOperators { get; set; } = AllowedArithmeticOperators.All;

        /// <summary>
        /// Gets or sets a list of allowed functions used in the $filter query.
        /// </summary>
        public AllowedFunctions AllowedFunctions { get; set; } = AllowedFunctions.All;

        /// <summary>
        /// Gets or sets a list of allowed logical operators in the $filter query.
        /// </summary>
        public AllowedLogicalOperators AllowedLogicalOperators { get; set; } = AllowedLogicalOperators.All;

        /// <summary>
        /// Gets or sets the query parameters that are allowed inside query. The default is all
        /// query options, including $filter, $skip, $top, $orderby, $expand, $select, $count,
        /// $format, $skiptoken and $search.
        /// </summary>
        public AllowedQueryOptions AllowedQueryOptions { get; set; } = AllowedQueryOptions.All;

        /// <summary>
        /// Gets or sets the max value of $skip that a client can request.
        /// </summary>
        public int? MaxSkip { get; set; }

        /// <summary>
        /// Gets or sets the max value of $top that a client can request.
        /// </summary>
        public int? MaxTop { get; set; }
    }
}