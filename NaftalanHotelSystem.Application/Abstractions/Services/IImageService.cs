using NaftalanHotelSystem.Application.DataTransferObject.Image;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.Abstractions.Services;

public interface IImageService
{
    Task<List<ImageDto>> GetImagesByEntityAsync(ImageEntity entity, int relatedEntityId);
    Task<ImageDto> UploadImageAsync(ImageCreateDto dto);
    Task DeleteImageAsync(int id);
}
public interface IFileService 
{
    string GetRootPath();
    string GetImagesPath();
    string GenerateUniqueFileName(string originalFileName);
}
