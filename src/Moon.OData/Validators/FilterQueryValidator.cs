using System.Runtime.CompilerServices;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;

namespace Moon.OData.Validators
{
    /// <summary>
    /// Represents a validator used to validate a $filter query option value.
    /// </summary>
    public class FilterQueryValidator
    {
        /// <summary>
        /// Validates the parsed $filter query option.
        /// </summary>
        /// <param name="filter">The parsed $filter query option.</param>
        /// <param name="settings">The validation settings.</param>
        public virtual void Validate(FilterClause filter, ODataValidationSettings settings)
        {
            Requires.NotNull(filter, nameof(filter));
            Requires.NotNull(settings, nameof(settings));

            ValidateQueryNode(filter.Expression, settings);
        }

        void ValidateQueryNode(QueryNode node, ODataValidationSettings settings)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack();

            var singleNode = node as SingleValueNode;
            var collectionNode = node as CollectionNode;

            if (singleNode != null)
            {
                ValidateSingleValueNode(singleNode, settings);
            }
            else if (collectionNode != null)
            {
                ValidateCollectionNode(collectionNode, settings);
            }
        }

        void ValidateSingleValueNode(SingleValueNode node, ODataValidationSettings settings)
        {
            switch (node.Kind)
            {
                case QueryNodeKind.BinaryOperator:
                    ValidateBinaryOperatorNode(node as BinaryOperatorNode, settings);
                    break;

                case QueryNodeKind.Convert:
                    ValidateQueryNode((node as ConvertNode).Source, settings);
                    break;

                case QueryNodeKind.SingleValuePropertyAccess:
                    ValidateQueryNode((node as SingleValuePropertyAccessNode).Source, settings);
                    break;

                case QueryNodeKind.UnaryOperator:
                    ValidateUnaryOperatorNode(node as UnaryOperatorNode, settings);
                    break;

                case QueryNodeKind.SingleValueFunctionCall:
                    ValidateSingleValueFunctionCallNode(node as SingleValueFunctionCallNode, settings);
                    break;

                case QueryNodeKind.SingleEntityFunctionCall:
                    ValidateSingleEntityFunctionCallNode(node as SingleEntityFunctionCallNode, settings);
                    break;

                case QueryNodeKind.SingleEntityCast:
                    ValidateQueryNode((node as SingleEntityCastNode).Source, settings);
                    break;

                case QueryNodeKind.Any:
                    ValidateAnyNode(node as AnyNode, settings);
                    break;

                case QueryNodeKind.All:
                    ValidateAllNode(node as AllNode, settings);
                    break;

                case QueryNodeKind.Constant:
                case QueryNodeKind.EntityRangeVariableReference:
                case QueryNodeKind.NonentityRangeVariableReference:
                case QueryNodeKind.SingleValueOpenPropertyAccess:
                    break;

                default:
                    throw new ODataException($"Validation of the '{node.Kind}' query node is not supported.");
            }
        }

        void ValidateCollectionNode(CollectionNode node, ODataValidationSettings settings)
        {
            switch (node.Kind)
            {
                case QueryNodeKind.CollectionPropertyAccess:
                    ValidateQueryNode((node as CollectionPropertyAccessNode).Source, settings);
                    break;

                case QueryNodeKind.EntityCollectionCast:
                    ValidateQueryNode((node as EntityCollectionCastNode).Source, settings);
                    break;

                default:
                    throw new ODataException($"Validation of the '{node.Kind}' query node is not supported.");
            }
        }

        void ValidateBinaryOperatorNode(BinaryOperatorNode node, ODataValidationSettings settings)
        {
            switch (node.OperatorKind)
            {
                case BinaryOperatorKind.Equal:
                case BinaryOperatorKind.NotEqual:
                case BinaryOperatorKind.And:
                case BinaryOperatorKind.GreaterThan:
                case BinaryOperatorKind.GreaterThanOrEqual:
                case BinaryOperatorKind.LessThan:
                case BinaryOperatorKind.LessThanOrEqual:
                case BinaryOperatorKind.Or:
                case BinaryOperatorKind.Has:
                    ValidateLogicalOperator(node, settings);
                    break;

                default:
                    ValidateArithmeticOperator(node, settings);
                    break;
            }
        }

        void ValidateUnaryOperatorNode(UnaryOperatorNode node, ODataValidationSettings settings)
        {
            ValidateQueryNode(node.Operand, settings);

            switch (node.OperatorKind)
            {
                case UnaryOperatorKind.Negate:
                case UnaryOperatorKind.Not:
                    if (!settings.AllowedLogicalOperators.HasFlag(AllowedLogicalOperators.Not))
                    {
                        throw new ODataException($"The '{node.OperatorKind}' logical operator is not allowed.");
                    }
                    break;

                default:
                    throw new ODataException($"Validation of the '{node.OperatorKind}' unary operator is not supported.");
            }
        }

        void ValidateSingleValueFunctionCallNode(SingleValueFunctionCallNode node, ODataValidationSettings settings)
        {
            ValidateFunction(node.Name, settings);

            foreach (var argumentNode in node.Parameters)
            {
                ValidateQueryNode(argumentNode, settings);
            }
        }

        void ValidateSingleEntityFunctionCallNode(SingleEntityFunctionCallNode node, ODataValidationSettings settings)
        {
            ValidateFunction(node.Name, settings);

            foreach (var argumentNode in node.Parameters)
            {
                ValidateQueryNode(argumentNode, settings);
            }
        }

        void ValidateNavigationPropertyNode(QueryNode node, ODataValidationSettings settings)
        {
            if (node != null)
            {
                ValidateQueryNode(node, settings);
            }
        }

        void ValidateAnyNode(AnyNode node, ODataValidationSettings settings)
        {
            ValidateFunction("any", settings);
            ValidateQueryNode(node.Source, settings);

            if (node.Body != null && node.Body.Kind != QueryNodeKind.Constant)
            {
                ValidateQueryNode(node.Body, settings);
            }
        }

        void ValidateAllNode(AllNode node, ODataValidationSettings settings)
        {
            ValidateFunction("all", settings);
            ValidateQueryNode(node.Source, settings);
            ValidateQueryNode(node.Body, settings);
        }

        void ValidateLogicalOperator(BinaryOperatorNode node, ODataValidationSettings settings)
        {
            var logicalOperator = ToLogicalOperator(node);

            if (!settings.AllowedLogicalOperators.HasFlag(logicalOperator))
            {
                throw new ODataException($"The '{logicalOperator}' logical operator is not allowed.");
            }

            ValidateQueryNode(node.Left, settings);
            ValidateQueryNode(node.Right, settings);
        }

        void ValidateArithmeticOperator(BinaryOperatorNode node, ODataValidationSettings settings)
        {
            var arithmeticOperator = ToArithmeticOperator(node);

            if (!settings.AllowedArithmeticOperators.HasFlag(arithmeticOperator))
            {
                throw new ODataException($"The '{arithmeticOperator}' arithmetic operator is not allowed.");
            }

            ValidateQueryNode(node.Left, settings);
            ValidateQueryNode(node.Right, settings);
        }

        void ValidateFunction(string functionName, ODataValidationSettings settings)
        {
            var function = ToFunction(functionName);

            if (!settings.AllowedFunctions.HasFlag(function))
            {
                throw new ODataException($"The '{functionName}' function is not allowed.");
            }
        }

        AllowedLogicalOperators ToLogicalOperator(BinaryOperatorNode binaryNode)
        {
            var result = AllowedLogicalOperators.None;

            switch (binaryNode.OperatorKind)
            {
                case BinaryOperatorKind.Equal:
                    result = AllowedLogicalOperators.Equal;
                    break;

                case BinaryOperatorKind.NotEqual:
                    result = AllowedLogicalOperators.NotEqual;
                    break;

                case BinaryOperatorKind.And:
                    result = AllowedLogicalOperators.And;
                    break;

                case BinaryOperatorKind.GreaterThan:
                    result = AllowedLogicalOperators.GreaterThan;
                    break;

                case BinaryOperatorKind.GreaterThanOrEqual:
                    result = AllowedLogicalOperators.GreaterThanOrEqual;
                    break;

                case BinaryOperatorKind.LessThan:
                    result = AllowedLogicalOperators.LessThan;
                    break;

                case BinaryOperatorKind.LessThanOrEqual:
                    result = AllowedLogicalOperators.LessThanOrEqual;
                    break;

                case BinaryOperatorKind.Or:
                    result = AllowedLogicalOperators.Or;
                    break;

                case BinaryOperatorKind.Has:
                    result = AllowedLogicalOperators.Has;
                    break;
            }

            return result;
        }

        AllowedArithmeticOperators ToArithmeticOperator(BinaryOperatorNode binaryNode)
        {
            var result = AllowedArithmeticOperators.None;

            switch (binaryNode.OperatorKind)
            {
                case BinaryOperatorKind.Add:
                    result = AllowedArithmeticOperators.Add;
                    break;

                case BinaryOperatorKind.Divide:
                    result = AllowedArithmeticOperators.Divide;
                    break;

                case BinaryOperatorKind.Modulo:
                    result = AllowedArithmeticOperators.Modulo;
                    break;

                case BinaryOperatorKind.Multiply:
                    result = AllowedArithmeticOperators.Multiply;
                    break;

                case BinaryOperatorKind.Subtract:
                    result = AllowedArithmeticOperators.Subtract;
                    break;
            }

            return result;
        }

        AllowedFunctions ToFunction(string functionName)
        {
            var result = AllowedFunctions.None;

            switch (functionName)
            {
                case "any":
                    result = AllowedFunctions.Any;
                    break;

                case "all":
                    result = AllowedFunctions.All;
                    break;

                case "cast":
                    result = AllowedFunctions.Cast;
                    break;

                case "ceiling":
                    result = AllowedFunctions.Ceiling;
                    break;

                case "concat":
                    result = AllowedFunctions.Concat;
                    break;

                case "contains":
                    result = AllowedFunctions.SubstringOf;
                    break;

                case "day":
                    result = AllowedFunctions.Day;
                    break;

                case "endswith":
                    result = AllowedFunctions.EndsWith;
                    break;

                case "floor":
                    result = AllowedFunctions.Floor;
                    break;

                case "hour":
                    result = AllowedFunctions.Hour;
                    break;

                case "indexof":
                    result = AllowedFunctions.IndexOf;
                    break;

                case "isof":
                    result = AllowedFunctions.IsOf;
                    break;

                case "length":
                    result = AllowedFunctions.Length;
                    break;

                case "minute":
                    result = AllowedFunctions.Minute;
                    break;

                case "month":
                    result = AllowedFunctions.Month;
                    break;

                case "round":
                    result = AllowedFunctions.Round;
                    break;

                case "second":
                    result = AllowedFunctions.Second;
                    break;

                case "startswith":
                    result = AllowedFunctions.StartsWith;
                    break;

                case "substring":
                    result = AllowedFunctions.Substring;
                    break;

                case "tolower":
                    result = AllowedFunctions.ToLower;
                    break;

                case "toupper":
                    result = AllowedFunctions.ToUpper;
                    break;

                case "trim":
                    result = AllowedFunctions.Trim;
                    break;

                case "year":
                    result = AllowedFunctions.Year;
                    break;

                case "date":
                    result = AllowedFunctions.Date;
                    break;

                case "time":
                    result = AllowedFunctions.Time;
                    break;

                case "fractionalseconds":
                    result = AllowedFunctions.FractionalSeconds;
                    break;
            }

            return result;
        }
    }
}