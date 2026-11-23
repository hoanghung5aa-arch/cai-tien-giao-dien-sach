using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace _31241027221_NguyenHoangHung.Validation;

public sealed class InvariantNumericModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        var type = context.Metadata.ModelType;
        if (type == typeof(double) || type == typeof(double?) || type == typeof(decimal) || type == typeof(decimal?))
        {
            return new InvariantNumericModelBinder();
        }

        return null;
    }
}

public sealed class InvariantNumericModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueResult);
        var rawValue = valueResult.FirstValue;
        if (string.IsNullOrWhiteSpace(rawValue))
        {
            return Task.CompletedTask;
        }

        if (bindingContext.ModelType == typeof(decimal) || bindingContext.ModelType == typeof(decimal?))
        {
            if (decimal.TryParse(rawValue, NumberStyles.Float, CultureInfo.InvariantCulture, out var decimalValue)
                || decimal.TryParse(rawValue, NumberStyles.Number, CultureInfo.CurrentCulture, out decimalValue))
            {
                bindingContext.Result = ModelBindingResult.Success(decimalValue);
                return Task.CompletedTask;
            }
        }
        else if (double.TryParse(rawValue, NumberStyles.Float, CultureInfo.InvariantCulture, out var doubleValue)
            || double.TryParse(rawValue, NumberStyles.Number, CultureInfo.CurrentCulture, out doubleValue))
        {
            if (double.IsFinite(doubleValue))
            {
                bindingContext.Result = ModelBindingResult.Success(doubleValue);
                return Task.CompletedTask;
            }
        }

        bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Giá trị số không hợp lệ.");
        bindingContext.Result = ModelBindingResult.Failed();
        return Task.CompletedTask;
    }
}
