using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TheWhiskyRealm.ModelBinders;

/// <summary>
/// Мodel binder provider that provides a model binder for double types.
/// </summary>
public class DoubleModelBinderProvider : IModelBinderProvider
{
    /// <summary>
    /// Gets a model binder based on context.
    /// </summary>
    /// <param name="context">The model binder provider context.</param>
    /// <returns>An instance of IModelBinder if the model type is double or nullable double; otherwise, null.</returns>
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if(context.Metadata.ModelType == typeof(double) ||
            context.Metadata.ModelType == typeof(double?))
        {
            return new DoubleModelBinder();
        }

        return null;
    }
}
