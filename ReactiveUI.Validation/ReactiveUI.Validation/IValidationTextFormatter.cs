namespace ReactiveUI.Validation
{
    /// <summary>
    /// Specification for a <see cref="ValidationText"/> FORMATTER.
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    public interface IValidationTextFormatter<out TOut>
    {
        TOut Format(ValidationText validationText);
    }
}