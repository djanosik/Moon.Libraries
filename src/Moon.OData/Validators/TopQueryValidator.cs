namespace Moon.OData.Validators
{
    /// <summary>
    /// Represents a validator used to validate a $top query option value.
    /// </summary>
    public class TopQueryValidator
    {
        /// <summary>
        /// Validates the parsed $top query option.
        /// </summary>
        /// <param name="top">The parsed $top query option.</param>
        /// <param name="settings">The validation settings.</param>
        public virtual void Validate(long? top, ODataValidationSettings settings)
        {
            Requires.NotNull(settings, nameof(settings));

            if (top > settings.MaxTop)
            {
                throw new ODataException($"The $top query option exceeded the ODataValidationSettiongs.MaxTop value.");
            }
        }
    }
}