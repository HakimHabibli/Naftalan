using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.Abstractions.UnitOfWork;
using NaftalanHotelSystem.Application.DataTransferObject.Image;
using NaftalanHotelSystem.Domain.Entites;
namespace NaftalanHotelSystem.Application.Concretes.Services;

public class ImageService : IImageService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;

    public ImageService(IUnitOfWork unitOfWork, IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }

    public async Task<ImageDto> UploadImageAsync(ImageCreateDto dto)
    {
        string folderPath = Path.Combine(_fileService.GetImagesPath(), dto.Entity.ToString().ToLower());

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string uniqueFileName = _fileService.GenerateUniqueFileName(dto.File.FileName);
        string fullPath = Path.Combine(folderPath, uniqueFileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await dto.File.CopyToAsync(stream);
        }

        var image = new Image
        {
            Url = Path.Combine("uploads", "images", dto.Entity.ToString().ToLower(), uniqueFileName).Replace("\\", "/"),
            Entity = dto.Entity,
            RelatedEntityId = dto.RelatedEntityId
        };

        await _unitOfWork.ImageWriteRepository.CreateAsync(image);
        await _unitOfWork.SaveChangesAsync();

        return new ImageDto
        {
            Id = image.Id,
            Url = image.Url,
            Entity = image.Entity,
            RelatedEntityId = image.RelatedEntityId
        };
    }

    public async Task<List<ImageDto>> GetImagesByEntityAsync(ImageEntity entity, int relatedEntityId)
    {
        var images = _unitOfWork.ImageReadRepository
            .GetAll()
            .Where(x => x.Entity == entity && x.RelatedEntityId == relatedEntityId)
            .ToList();

        return images.Select(x => new ImageDto
        {
            Id = x.Id,
            Url = x.Url,
            Entity = x.Entity,
            RelatedEntityId = x.RelatedEntityId
        }).ToList();
    }

    public async Task DeleteImageAsync(int id)
    {
        var image = await _unitOfWork.ImageReadRepository.GetByIdAsync(id);
        if (image is null) return;

        string fullPath = Path.Combine(_fileService.GetRootPath(), image.Url.Replace("/", "\\"));
        if (File.Exists(fullPath))
            File.Delete(fullPath);

        _unitOfWork.ImageWriteRepository.Remove(image);
        await _unitOfWork.SaveChangesAsync();
    }
}

    //public async Task<ImageDto> UploadImageAsync(ImageCreateDto dto)
    //{
    //    string folder = Path.Combine("wwwroot", "uploads", dto.Entity.ToLower());
    //    if (!Directory.Exists(folder))
    //        Directory.CreateDirectory(folder);

    //    string fileName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
    //    string fullPath = Path.Combine(folder, fileName);

    //    using (var stream = new FileStream(fullPath, FileMode.Create))
    //    {
    //        await dto.File.CopyToAsync(stream);
    //    }

    //    var image = new Image
    //    {
    //        Url = Path.Combine("uploads", dto.Entity.ToLower(), fileName).Replace("\\", "/"),
    //        Entity = Enum.Parse<ImageEntity>(dto.Entity),
    //        RelatedEntityId = dto.RelatedEntityId
    //    };

    //    await _unitOfWork.ImageWriteRepository.CreateAsync(image);
    //    await _unitOfWork.SaveChangesAsync();

    //    return new ImageDto
    //    {
    //        Id = image.Id,
    //        Url = image.Url,
    //        Entity = image.Entity.ToString(),
    //        RelatedEntityId = image.RelatedEntityId
    //    };
    //}

    //public async Task<List<ImageDto>> GetImagesByEntityAsync(string entity, int relatedEntityId)
    //{
    //    var images = _unitOfWork.ImageReadRepository
    //        .GetAll()
    //        .Where(x => x.Entity.ToString() == entity && x.RelatedEntityId == relatedEntityId)
    //        .ToList();

    //    return images.Select(x => new ImageDto
    //    {
    //        Id = x.Id,
    //        Url = x.Url,
    //        Entity = x.Entity.ToString(),
    //        RelatedEntityId = x.RelatedEntityId
    //    }).ToList();
    //}

    //public async Task DeleteImageAsync(int id)
    //{
    //    var image = await _unitOfWork.ImageReadRepository.GetByIdAsync(id);
    //    if (image is null) return;

    //    string fullPath = Path.Combine(_env.WebRootPath, image.Url);
    //    if (File.Exists(fullPath))
    //        File.Delete(fullPath);

    //    _unitOfWork.ImageWriteRepository.Remove(image);
    //    await _unitOfWork.SaveChangesAsync();
    //}

