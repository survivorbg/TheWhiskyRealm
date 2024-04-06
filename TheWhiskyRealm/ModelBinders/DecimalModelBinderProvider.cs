using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TheWhiskyRealm.ModelBinders;

/// <summary>
/// The DecimalModelBinderProvider class is a model binder provider that provides a model binder for decimal types.
/// </summary>
public class DecimalModelBinderProvider : IModelBinderProvider
{
    /// <summary>
    /// Gets a model binder based on context.
    /// </summary>
    /// <param name="context">The model binder provider context.</param>
    /// <returns>An instance of IModelBinder if the model type is decimal or nullable decimal; otherwise, null.</returns>
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(decimal) ||
            context.Metadata.ModelType == typeof(decimal?))
        {
            return new DecimalModelBinder();
        }

        return null;
    }
}
