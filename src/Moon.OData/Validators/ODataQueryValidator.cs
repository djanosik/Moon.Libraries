namespace Moon.OData.Validators
{
    /// <summary>
    /// Represents a validator used to validate OData queries based on the <see cref="ODataValidationSettings" />.
    /// </summary>
    public class ODataQueryValidator
    {
        readonly FilterQueryValidator filterQueryValidator = new FilterQueryValidator();
        readonly SkipQueryValidator skipQueryValidator = new SkipQueryValidator();
        readonly TopQueryValidator topQueryValidator = new TopQueryValidator();

        /// <summary>
        /// Validates the given OData query using the specified validation settings..
        /// </summary>
        /// <typeparam name="TEntity">The type of entity.</typeparam>
        /// <param name="odata">The OData query to validate.</param>
        /// <param name="settings">The validation settings.</param>
        public virtual void Validate<TEntity>(ODataQuery<TEntity> odata, ODataValidationSettings settings)
        {
            Requires.NotNull(odata, nameof(odata));
            Requires.NotNull(settings, nameof(settings));

            if (odata.Count != null)
            {
                ValidateAllowed(AllowedQueryOptions.Count, settings.AllowedQueryOptions);
            }

            if (odata.RawValues.DeltaToken != null)
            {
                ValidateAllowed(AllowedQueryOptions.DeltaToken, settings.AllowedQueryOptions);
            }

            if (odata.RawValues.Format != null)
            {
                ValidateAllowed(AllowedQueryOptions.Format, settings.AllowedQueryOptions);
            }

            if (odata.Filter != null)
            {
                ValidateAllowed(AllowedQueryOptions.Filter, settings.AllowedQueryOptions);
                filterQueryValidator.Validate(odata.Filter, settings);
            }

            if (odata.OrderBy != null)
            {
                ValidateAllowed(AllowedQueryOptions.OrderBy, settings.AllowedQueryOptions);
            }

            if (odata.Search != null)
            {
                ValidateAllowed(AllowedQueryOptions.Search, settings.AllowedQueryOptions);
            }

            if (odata.RawValues.Select != null)
            {
                ValidateAllowed(AllowedQueryOptions.Select, settings.AllowedQueryOptions);
            }

            if (odata.RawValues.Expand != null)
            {
                ValidateAllowed(AllowedQueryOptions.Expand, settings.AllowedQueryOptions);
            }

            if (odata.Skip != null)
            {
                ValidateAllowed(AllowedQueryOptions.Skip, settings.AllowedQueryOptions);
                skipQueryValidator.Validate(odata.Skip, settings);
            }

            if (odata.RawValues.SkipToken != null)
            {
                ValidateAllowed(AllowedQueryOptions.SkipToken, settings.AllowedQueryOptions);
            }

            if (odata.Top != null)
            {
                ValidateAllowed(AllowedQueryOptions.Top, settings.AllowedQueryOptions);
                topQueryValidator.Validate(odata.Top, settings);
            }
        }

        void ValidateAllowed(AllowedQueryOptions option, AllowedQueryOptions allowed)
        {
            if (!allowed.HasFlag(option))
            {
                throw new ODataException($"The '{option}' query option is not allowed.");
            }
        }
    }
}