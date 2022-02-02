namespace NorthWind.Entities.Interfaces;

public interface IValidator<T>
{
    ValueTask<bool> Validate(T instaceToValidate);
    IEnumerable<KeyValuePair<string, string>> Failures { get; }
}
