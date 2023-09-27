using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace BackEnd_Angular.Utilities
{
	public class TypeBinder<T> : IModelBinder
	{
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			var PropertieName = bindingContext.ModelName;
			var value = bindingContext.ValueProvider.GetValue(PropertieName);

			if (value == ValueProviderResult.None)
			{
				return Task.CompletedTask;
			}

			try
			{
				var deserializedValue = JsonConvert.DeserializeObject<T>(value.FirstValue);
				bindingContext.Result = ModelBindingResult.Success(deserializedValue);
			}
			catch
			{
				bindingContext.ModelState.TryAddModelError(PropertieName, "The given value is not the appropriate type");
			}

			return Task.CompletedTask;
		}
	}
}
