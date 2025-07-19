using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.Abstractions.UnitOfWork;
using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Application.DataTransferObject.Illness;
using NaftalanHotelSystem.Application.DataTransferObject.Image;
using NaftalanHotelSystem.Domain.Entites;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.Concretes.Services;
#region TEST
/*
 public class IllnessService:IIllnessService
{
    private readonly IUnitOfWork _unitOfWork;

    public IllnessService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CreateIllnessAsync(IllnessCreateDto dto)
    {
        var illness = new Illness
        {
            TreatmentCategoryId = dto.TreatmentCategoryId,
            Translations = dto.Translations.Select(t => new IllnessTranslation
            {
                Name = t.Name,
                Description = t.Description,
                Language = t.Language
            }).ToList()
        };

        await _unitOfWork.IllnessWriteRepository.CreateAsync(illness);
        await _unitOfWork.SaveChangesAsync();
        return illness.Id;
    }

    public async Task UpdateIllnessAsync(int id, IllnessUpdateDto dto)
    {
        var illness = await _unitOfWork.IllnessWriteRepository.Table
            .Include(i => i.Translations)
            .FirstOrDefaultAsync(i => i.Id == id);

        illness.TreatmentCategoryId = dto.TreatmentCategoryId;

       
        illness.Translations = dto.Translations.Select(t => new IllnessTranslation
        {
            Name = t.Name,
            Description = t.Description,
            Language = t.Language
        }).ToList();

        _unitOfWork.IllnessWriteRepository.Update(illness);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteIllnessAsync(int id)
    {
        var illness = await _unitOfWork.IllnessWriteRepository.Table
            .FirstOrDefaultAsync(i => i.Id == id);

       

        _unitOfWork.IllnessWriteRepository.Remove(illness);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<IllnessDto>> GetAllIllnessesAsync()
    {
        var illnesses = await _unitOfWork.IllnessReadRepository.Table
            .Include(i => i.Translations)
            .Include(i => i.TreatmentCategory).ThenInclude(tc => tc.Translations)
            .ToListAsync();

        return illnesses.Select(i => new IllnessDto
        {
            Id = i.Id,
            TreatmentCategoryId = i.TreatmentCategoryId,
            Translations = i.Translations.Select(t => new IllnessTranslationDto
            {
                Name = t.Name,
                Description = t.Description,
                Language = t.Language
            }).ToList()
        }).ToList();
    }

    public async Task<IllnessGetDto> GetIllnessByIdAsync(int id)
    {
        var illness = await _unitOfWork.IllnessReadRepository.Table
            .Include(i => i.Translations)
            .Include(i => i.TreatmentCategory)
                .ThenInclude(tc => tc.Translations)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (illness == null)
        {
            throw new Exception($"Illness with Id {id} not found");
        }

       
        var illnessTranslations = illness.Translations.Select(t => new IllnessTranslationDto
        {
            Name = t.Name,
            Description = t.Description,
            Language = t.Language
        }).ToList();

       
        var treatmentCategoryTranslations = illness.TreatmentCategory?.Translations.Select(tc => new TreatmentCategoryTranslationDto
        {
            Name = tc.Name,
            Language = tc.Language
        }).ToList() ?? new List<TreatmentCategoryTranslationDto>();

        return new IllnessGetDto
        {
            Id = illness.Id,
            TreatmentCategoryId = illness.TreatmentCategoryId,
            Translations = illnessTranslations,
            TreatmentCategoryTranslations = treatmentCategoryTranslations
        };
    }
}

 */
#endregion

public class IllnessService : IIllnessService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService; 

    public IllnessService(IUnitOfWork unitOfWork, IImageService imageService) 
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    // --- CREATE ILLNESS ---
    public async Task<int> CreateIllnessAsync(IllnessCreateDto dto)
    {
        var illness = new Illness
        {
            TreatmentCategoryId = dto.TreatmentCategoryId,
            Translations = dto.Translations.Select(t => new IllnessTranslation
            {
                Name = t.Name,
                Description = t.Description,
                Language = t.Language
            }).ToList()
        };

        await _unitOfWork.IllnessWriteRepository.CreateAsync(illness);
        await _unitOfWork.SaveChangesAsync(); // Illness ID-ni əldə etmək üçün SaveChangesAsync çağırılmalıdır

        // Şəkli yükləyin (Əgər varsa)
        if (dto.ImageFile != null)
        {
            var imageCreateDto = new ImageCreateDto
            {
                File = dto.ImageFile,
                Entity = ImageEntity.Illness, // ImageEntity enum-da Illness əlavə etdiyinizdən əmin olun
                RelatedEntityId = illness.Id // Yeni yaradılan xəstəliyin ID-si
            };
            await _imageService.UploadImageAsync(imageCreateDto); // ImageService öz daxilində SaveChangesAsync-i çağırır
        }

        return illness.Id;
    }

    // --- UPDATE ILLNESS ---
    public async Task UpdateIllnessAsync(int id, IllnessUpdateDto dto)
    {
        var illness = await _unitOfWork.IllnessWriteRepository.Table
            .Include(i => i.Translations)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (illness == null)
        {
            throw new Exception($"Illness with Id {id} not found for update.");
        }

        illness.TreatmentCategoryId = dto.TreatmentCategoryId;

        // Tərcümələri yeniləmək məntiqi (RoomService-dəki kimi)
        var currentTranslations = illness.Translations.ToList();
        var translationsToDelete = currentTranslations
            .Where(ct => !dto.Translations.Any(dt => dt.Id == ct.Id)) // DTO-da olmayanları sil (Əgər TranslationUpdateDto-da Id varsa)
            .ToList();
        _unitOfWork.IllnessTranslationWriteRepository.RemoveRange(translationsToDelete);

        foreach (var newOrUpdatedTranslationDto in dto.Translations)
        {
            // Fərz edirik ki, TranslationsUpdateDto-da həm də ID var
            var existingTranslation = currentTranslations.FirstOrDefault(ct => ct.Id == newOrUpdatedTranslationDto.Id);
            if (existingTranslation != null)
            {
                // Mövcud tərcüməni yenilə
                existingTranslation.Name = newOrUpdatedTranslationDto.Name;
                existingTranslation.Description = newOrUpdatedTranslationDto.Description;
                existingTranslation.Language = newOrUpdatedTranslationDto.Language;
                _unitOfWork.IllnessTranslationWriteRepository.Update(existingTranslation);
            }
            else
            {
                // Yeni tərcüməni əlavə et
                var newTranslation = new IllnessTranslation
                {
                    IllnessId = illness.Id, // Xəstəliyin ID-sini təyin et
                    Name = newOrUpdatedTranslationDto.Name,
                    Description = newOrUpdatedTranslationDto.Description,
                    Language = newOrUpdatedTranslationDto.Language
                };
                await _unitOfWork.IllnessTranslationWriteRepository.CreateAsync(newTranslation);
            }
        }

        // --- Şəkil Yeniləmə Məntiqi ---
        // Əgər yeni şəkil faylı göndərilibsə, mövcud şəkli sil və yenisini əlavə et
        if (dto.ImageFile != null)
        {
            // Mövcud şəkli tapın və silin
            var existingImage = await _unitOfWork.ImageReadRepository.GetAll(asNoTracking: true)
                .Where(x => x.Entity == ImageEntity.Illness && x.RelatedEntityId == illness.Id)
                .FirstOrDefaultAsync();

            if (existingImage != null)
            {
                await _imageService.DeleteImageAsync(existingImage.Id); // Köhnə şəkli sil
            }

            // Yeni şəkli yükləyin
            var imageCreateDto = new ImageCreateDto
            {
                File = dto.ImageFile,
                Entity = ImageEntity.Illness,
                RelatedEntityId = illness.Id
            };
            await _imageService.UploadImageAsync(imageCreateDto);
        }

        _unitOfWork.IllnessWriteRepository.Update(illness); // Əsas illness obyektini yeniləyin
        await _unitOfWork.SaveChangesAsync(); // Bütün dəyişiklikləri yadda saxlayın
    }

    // --- DELETE ILLNESS ---
    public async Task DeleteIllnessAsync(int id)
    {
        var illness = await _unitOfWork.IllnessWriteRepository.Table
            .Include(i => i.Translations) // Tərcümələri də silmək üçün yükləyirik
            .FirstOrDefaultAsync(i => i.Id == id);

        if (illness == null)
        {
            throw new Exception($"Illness with Id {id} not found for deletion.");
        }

        // Xəstəliyə aid bütün şəkilləri tapın və ImageService vasitəsilə silin
        var imagesToDelete = await _unitOfWork.ImageReadRepository.GetAll(asNoTracking: true)
            .Where(x => x.Entity == ImageEntity.Illness && x.RelatedEntityId == illness.Id)
            .ToListAsync();

        foreach (var image in imagesToDelete)
        {
            await _imageService.DeleteImageAsync(image.Id); // ImageService öz daxilində SaveChangesAsync-i çağırır
        }

        // Xəstəliyin tərcümələrini silin
        if (illness.Translations != null && illness.Translations.Any())
        {
            _unitOfWork.IllnessTranslationWriteRepository.RemoveRange(illness.Translations.ToList());
        }

        _unitOfWork.IllnessWriteRepository.Remove(illness); // Xəstəliyin özünü silin
        await _unitOfWork.SaveChangesAsync(); // Bütün qalan dəyişiklikləri vahid transaksiyada yadda saxlayın
    }

    // --- GET ALL ILLNESSES ---
    public async Task<List<IllnessGetDto>> GetAllIllnessesAsync() 
    {
        var illnesses = await _unitOfWork.IllnessReadRepository.Table
            .Include(i => i.Translations)
            .Include(i => i.TreatmentCategory).ThenInclude(tc => tc.Translations)
            .ToListAsync();

        var illnessGetDtos = new List<IllnessGetDto>();

        foreach (var illness in illnesses)
        {
            // Hər bir xəstəlik üçün şəkilləri yükləyin (yalnız bir şəkil gözləyirik)
            var illnessImageUrl = await _unitOfWork.ImageReadRepository.GetAll(asNoTracking: true)
                .Where(x => x.Entity == ImageEntity.Illness && x.RelatedEntityId == illness.Id)
                .Select(x => x.Url)
                .FirstOrDefaultAsync();

            illnessGetDtos.Add(new IllnessGetDto
            {
                Id = illness.Id,
                TreatmentCategoryId = illness.TreatmentCategoryId,
                Translations = illness.Translations.Select(t => new IllnessTranslationDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    Language = t.Language
                }).ToList(),
                TreatmentCategoryTranslations = illness.TreatmentCategory?.Translations.Select(tc => new TreatmentCategoryTranslationDto
                {
                    Id = tc.Id,
                    Name = tc.Name,
                    Language = tc.Language
                }).ToList() ?? new List<TreatmentCategoryTranslationDto>(),
                ImageUrls = illnessImageUrl 
            });
        }
        return illnessGetDtos;
    }

    // --- GET ILLNESS BY ID ---
    public async Task<IllnessGetDto> GetIllnessByIdAsync(int id)
    {
        var illness = await _unitOfWork.IllnessReadRepository.Table
            .Include(i => i.Translations)
            .Include(i => i.TreatmentCategory)
                .ThenInclude(tc => tc.Translations)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (illness == null)
        {
            return null; // Throw new Exception($"Illness with Id {id} not found"); - Controller exception-u tutmalıdır
        }

        // Xəstəliyə aid şəkli yükləyin (yalnız bir dənə)
        var illnessImageUrl = await _unitOfWork.ImageReadRepository.GetAll(asNoTracking: true)
            .Where(x => x.Entity == ImageEntity.Illness && x.RelatedEntityId == illness.Id)
            .Select(x => x.Url)
            .FirstOrDefaultAsync(); // Yalnız ilk şəklin URL-ini götürürük

        var illnessTranslations = illness.Translations.Select(t => new IllnessTranslationDto
        {
            Id=t.Id,
            Name = t.Name,
            Description = t.Description,
            Language = t.Language
        }).ToList();

        var treatmentCategoryTranslations = illness.TreatmentCategory?.Translations.Select(tc => new TreatmentCategoryTranslationDto
        {
            Id= tc.Id,
            Name = tc.Name,
            Language = tc.Language
        }).ToList() ?? new List<TreatmentCategoryTranslationDto>();

        return new IllnessGetDto
        {
            Id = illness.Id,
            TreatmentCategoryId = illness.TreatmentCategoryId,
            Translations = illnessTranslations,
            TreatmentCategoryTranslations = treatmentCategoryTranslations,
            ImageUrls = illnessImageUrl // Tək şəkil URL-ini DTO-ya əlavə edirik
        };
    }

 
}
