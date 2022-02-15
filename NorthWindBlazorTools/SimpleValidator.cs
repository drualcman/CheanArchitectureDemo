namespace NorthWind.BlazorTools;

public class SimpleValidator : ComponentBase
{
    [Inject]
    public IServiceProvider ServiceProvider { get; set; }

    [CascadingParameter]
    public EditContext EditContext { get; set; }

    ValidationMessageStore ValidationMessageStore;

    private async ValueTask<List<KeyValuePair<string, string>>> Validate(object model)
    {
        List<KeyValuePair<string, string>> faliures = null;

        Type modelType = typeof(IValidator<>).MakeGenericType(model.GetType());
        var serviceValidator = ServiceProvider.GetService(modelType);
        string validateMethodNAme = nameof(IValidator<object>.Validate);
        MethodInfo methodInfo = serviceValidator.GetType().GetMethods()
            .FirstOrDefault(m=>m.Name == validateMethodNAme && m.ReturnType == typeof(ValueTask<bool>));

        bool isValid = await (ValueTask<bool>)methodInfo.Invoke(serviceValidator, new object[] { model });
        if (!isValid)
        {
            PropertyInfo propertyInfo = serviceValidator.GetType().GetProperty(nameof(IValidator<object>.Failures));

            faliures = propertyInfo.GetValue(serviceValidator, null) as List<KeyValuePair<string, string>>;
        }
        return faliures;
    }

    FieldIdentifier GetFieldIdentifier(object model, string propertyName)
    {
        object newModel = model;
        string newPropertyName = propertyName;

        if (propertyName.Contains("["))
        {
            string collectionPropertyName = propertyName.Substring(0, propertyName.IndexOf("["));
            PropertyInfo propertyInfo = EditContext.Model.GetType().GetProperty(collectionPropertyName);
            if (propertyInfo != null)
            {
                var propertyModel = propertyInfo.GetValue(EditContext.Model) as IEnumerable<object>;
                int startIndexPosition = propertyName.IndexOf("[") + 1;
                int endIndexPosition = propertyName.IndexOf("]");
                int index = int.Parse(propertyName.Substring(startIndexPosition, endIndexPosition - startIndexPosition));
                newModel = propertyModel.ElementAt(index);
                newPropertyName = propertyName.Substring(propertyName.LastIndexOf(".") + 1);
            }
        }
        return new FieldIdentifier(newModel, newPropertyName);
    }

    void AddValidationResult(object model, List<KeyValuePair<string, string>> failures)
    {
        if (failures != null && failures.Any())
        {
            foreach (KeyValuePair<string, string> failure in failures)
            {
                FieldIdentifier fieldIdentifier = GetFieldIdentifier(model, failure.Key);
                ValidationMessageStore.Add(fieldIdentifier, failure.Value);
            }
        }
        EditContext.NotifyValidationStateChanged();
    }

    async void ValidationRequested(object sender, ValidationRequestedEventArgs args)
    {
        ValidationMessageStore.Clear();
        List<KeyValuePair<string, string>> failures = await Validate(EditContext.Model);
        AddValidationResult(EditContext.Model, failures);
    }

    async void FieldChanged(object sender, FieldChangedEventArgs args)
    {
        FieldIdentifier fieldIdentifier = args.FieldIdentifier;
        ValidationMessageStore.Clear(fieldIdentifier);
        List<KeyValuePair<string, string>> failures = await Validate(fieldIdentifier.Model);
        if (failures != null && failures.Any())
        {
            failures = failures.Where(kvp => kvp.Key == fieldIdentifier.FieldName).ToList();
        }
        AddValidationResult(fieldIdentifier.Model, failures);
    }

    private void SetEditContextEvents()
    {
        EditContext.OnValidationRequested += ValidationRequested;
        EditContext.OnFieldChanged += FieldChanged;
    }

    void EditcontextChanged()
    {
        ValidationMessageStore = new ValidationMessageStore(EditContext);
        SetEditContextEvents();
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        EditContext previousEditContext = EditContext;

        await base.SetParametersAsync(parameters);

        if (EditContext == null)
        {
            throw new NullReferenceException($"El validador debe ser colocado dentro de un EditForm");
        }

        if (EditContext != previousEditContext)
        {
            EditcontextChanged();
        }
    }
}
