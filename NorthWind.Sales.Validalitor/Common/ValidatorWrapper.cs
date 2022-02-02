namespace NorthWind.Sales.Validalitor.Common;

public abstract class ValidatorWrapper<T> : AbstractValidator<T>, Entities.Interfaces.IValidator<T>
{
    public IEnumerable<KeyValuePair<string, string>> Failures { get; private set; }

    public async new ValueTask<bool> Validate(T instaceToValidate)
    {
        ValidationResult result = await ValidateAsync(instaceToValidate);
        if (!result.IsValid)
        {
            Failures = result.Errors
                .Select(e => new KeyValuePair<string, string>(e.PropertyName, e.ErrorMessage)).ToList();
        }
        return result.IsValid;
    }
}
