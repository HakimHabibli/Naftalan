using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NaftalanHotelSystem.Application.DataTransferObject;
using Newtonsoft.Json;

namespace NaftalanHotelSystem.API.ModelBinders;

public class AboutUpdateDtoModelBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var form = bindingContext.HttpContext.Request.Form;

        var idStr = form["Id"].FirstOrDefault();
        var videoLink = form["VideoLink"].FirstOrDefault();
        var translationsJson = form["Translations"].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(translationsJson))
        {
            bindingContext.ModelState.AddModelError("Translations", "Translations are required.");
            bindingContext.Result = ModelBindingResult.Failed();
            return;
        }

        List<AboutTranslationUpdateDto> translations;
        try
        {
            translations = JsonConvert.DeserializeObject<List<AboutTranslationUpdateDto>>(translationsJson);
        }
        catch (Exception ex)
        {
            bindingContext.ModelState.AddModelError("Translations", $"Translations JSON is invalid: {ex.Message}");
            bindingContext.Result = ModelBindingResult.Failed();
            return;
        }

        var dto = new BinableAboutUpdateDto // Burda BinableAboutUpdateDto yaradılır
        {
            Id = int.TryParse(idStr, out var parsedId) ? parsedId : 0,
            VideoLink = videoLink,
            ImageFile = form.Files["ImageFile"],
            Translations = translationsJson // Translations string olaraq saxlanılır BinableAboutUpdateDto-da
        };

        bindingContext.Result = ModelBindingResult.Success(dto);
    }
}
[ModelBinder(BinderType = typeof(AboutUpdateDtoModelBinder))]
public class BinableAboutUpdateDto
{
    public int Id { get; set; }
    public string VideoLink { get; set; }
    public IFormFile ImageFile { get; set; }
    public string Translations { get; set; } // string olaraq gəlir
}
