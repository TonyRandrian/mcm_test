using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class JsonListModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var values = bindingContext.ValueProvider
            .GetValue(bindingContext.ModelName);

        if (values == ValueProviderResult.None)
        {
            bindingContext.Result = ModelBindingResult.Success(
                Activator.CreateInstance(bindingContext.ModelType));
            return Task.CompletedTask;
        }

        var elementType = bindingContext.ModelType.GenericTypeArguments[0];
        var list = (System.Collections.IList)Activator.CreateInstance(
            typeof(List<>).MakeGenericType(elementType))!;

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        foreach (var raw in values)
        {
            try
            {
                var item = JsonSerializer.Deserialize(raw, elementType, options);
                if (item is not null) list.Add(item);
            }
            catch (JsonException)
            {
                bindingContext.ModelState.AddModelError(
                    bindingContext.ModelName, $"Invalid JSON item: {raw}");
                return Task.CompletedTask;
            }
        }

        bindingContext.Result = ModelBindingResult.Success(list);
        return Task.CompletedTask;
    }
}