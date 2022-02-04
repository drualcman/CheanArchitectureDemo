namespace NorthWind.Entities.Services;

public class ValidationService<T>
{
    readonly IEnumerable<IValidator<T>> Validators;

    public ValidationService(IEnumerable<IValidator<T>> validators)
    {
        Validators = validators;
    }

    public async ValueTask ExecuteValidadationsGuard(T instanceToValidate, bool stopOnFirstValidatorError = true)
    {
        List<KeyValuePair<string, string>> failures = new List<KeyValuePair<string, string>>();
        bool continueValidation = true;
        IEnumerator<IValidator<T>> enumerator = Validators.GetEnumerator();
        while (enumerator.MoveNext() && continueValidation)
        {
            bool isValid = await enumerator.Current.Validate(instanceToValidate);
            if (!isValid)
            {
                failures.AddRange(enumerator.Current.Failures);
                continueValidation = !stopOnFirstValidatorError;
            }
        }
        if (failures.Count > 0)
        {
            string error = string.Join(" ", failures.Select(e => $"{e.Key}: {e.Value}").ToArray());
            throw new Exception($"Error de validacionL {error}");
        }
    }
}
