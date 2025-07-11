using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.Abstractions.UnitOfWork;
using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Infrastructure.Services;
public class RoomService : IRoomService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    public RoomService(IUnitOfWork unitOfWork, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task<List<RoomGetDto>> GetAllRoomsAsync()
    {
        // Otaqları tərcümələri və avadanlıqları ilə birlikdə yükləyin
        // AsNoTracking istifadə edirik, çünki bu, yalnız oxuma əməliyyatıdır və performansı artırır.
        var rooms = await _unitOfWork.RoomReadRepository.GetAll(asNoTracking: true)
            .Include(r => r.RoomTranslations)
            .Include(r => r.Equipments) // Equipments kolleksiyası yüklenir
            .ToListAsync();

        var roomDtos = new List<RoomGetDto>();

        foreach (var room in rooms)
        {
            // Hər otaq üçün şəkilləri ayrı-ayrı yükləyin
            var roomImages = await _unitOfWork.ImageReadRepository.GetAll(asNoTracking: true)
                .Where(x => x.Entity == ImageEntity.Room && x.RelatedEntityId == room.Id)
                .ToListAsync();

            roomDtos.Add(new RoomGetDto
            {
                Id = room.Id,
                Category = room.Category,
                Area = room.Area,
                Price = room.Price,
                Member = room.Member,
                YoutubeVideoLink = room.YoutubeVideoLink,
                Translations = room.RoomTranslations.Select(t => new RoomTranslationGetDto
                {
                    Id = t.Id, // BaseEntity-dən gəlir
                    Service = t.Service,
                    Description = t.Description,
                    MiniDescription = t.MiniDescription,
                    Title = t.Title,
                    MiniTitle = t.MiniTitle,
                    Language = t.Language // enum tipi
                }).ToList(),
                EquipmentIds = room.Equipments.Select(re => re.EquipmentId).ToList(),
                ImageUrls = roomImages.Select(img => img.Url).ToList()
            });
        }
        return roomDtos;
    }

    public async Task<RoomGetDto> GetRoomByIdAsync(int id)
    {
        // Otağı tərcümələri və avadanlıqları ilə birlikdə yükləyin
        var room = await _unitOfWork.RoomReadRepository.GetAll(asNoTracking: true)
            .Include(r => r.RoomTranslations)
            .Include(r => r.Equipments)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (room == null)
        {
            return null; // Otaq tapılmadıqda null qaytarın və ya xəta atın
        }

        // Otağa aid şəkilləri yükləyin
        var roomImages = await _unitOfWork.ImageReadRepository.GetAll(asNoTracking: true)
            .Where(x => x.Entity == ImageEntity.Room && x.RelatedEntityId == room.Id)
            .ToListAsync();

        return new RoomGetDto
        {
            Id = room.Id,
            Category = room.Category,
            Area = room.Area,
            Price = room.Price,
            Member = room.Member,
            YoutubeVideoLink = room.YoutubeVideoLink,
            Translations = room.RoomTranslations.Select(t => new RoomTranslationGetDto
            {
                Id = t.Id,
                Service = t.Service,
                Description = t.Description,
                MiniDescription = t.MiniDescription,
                Title = t.Title,
                MiniTitle = t.MiniTitle,
                Language = t.Language
            }).ToList(),
            EquipmentIds = room.Equipments.Select(re => re.EquipmentId).ToList(),
            ImageUrls = roomImages.Select(img => img.Url).ToList()
        };
    }

    public async Task CreateRoomAsync(RoomCreateDto dto)
    {
        var room = new Room
        {
            Category = dto.Category, // string
            Area = dto.Area,         // short
            Member = dto.Member,     // short
            Price = dto.Price,       // double
            YoutubeVideoLink = dto.YoutubeVideoLink,

            // RoomTranslations-ı yaradarkən, RoomTranslationCreateDto-dan map edin
            RoomTranslations = dto.Translations.Select(t => new RoomTranslation
            {
                Service = t.Service,
                Description = t.Description,
                MiniDescription = t.MiniDescription,
                MiniTitle = t.MiniTitle,
                Title = t.Title,
                Language = t.Language // Language enum
            }).ToList(),

            // Equipments-ı yaradarkən, EquipmentIds-dən map edin
            Equipments = dto.EquipmentIds.Select(id => new RoomEquipment { EquipmentId = id }).ToList()
        };

        await _unitOfWork.RoomWriteRepository.CreateAsync(room);
        // CreateAsync içində SaveChangesAsync olmadığı üçün, burada SaveChangesAsync çağırılmalıdır.
        // Bu, room.Id-nin generation olunmasını təmin edir, bu da şəkil yükləmək üçün vacibdir.
        await _unitOfWork.SaveChangesAsync();

        // Şəkilləri yükləyin
        if (dto.ImageFiles != null && dto.ImageFiles.Any())
        {
            foreach (var file in dto.ImageFiles)
            {
                var imageCreateDto = new ImageCreateDto
                {
                    File = file,
                    Entity = ImageEntity.Room,
                    RelatedEntityId = room.Id // Yeni yaradılan otağın ID-si
                };
                await _imageService.UploadImageAsync(imageCreateDto); // ImageService öz daxilində SaveChangesAsync-i çağırır
            }
        }
    }

    public async Task DeleteRoomAsync(int id)
    {
        // Otağı tərcümələri və avadanlıq əlaqələri ilə birlikdə yükləyin (izlənilən rejimdə)
        var room = await _unitOfWork.RoomReadRepository.GetAll(asNoTracking: false) // Update/Delete üçün izləmə vacibdir
            .Include(r => r.RoomTranslations)
            .Include(r => r.Equipments)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (room == null)
        {
            throw new Exception("Otaq tapılmadı.");
        }

        // Otağa aid bütün şəkilləri tapın və ImageService vasitəsilə silin
        var imagesToDelete = await _unitOfWork.ImageReadRepository.GetAll(asNoTracking: true)
            .Where(x => x.Entity == ImageEntity.Room && x.RelatedEntityId == room.Id)
            .ToListAsync();

        foreach (var image in imagesToDelete)
        {
            await _imageService.DeleteImageAsync(image.Id); // ImageService öz daxilində SaveChangesAsync-i çağırır
        }

        // Otağın tərcümələrini silin
        if (room.RoomTranslations != null && room.RoomTranslations.Any())
        {
            _unitOfWork.RoomTranslationWriteRepository.RemoveRange(room.RoomTranslations.ToList());
        }

        // Otağın avadanlıq əlaqələrini silin
        if (room.Equipments != null && room.Equipments.Any())
        {
            _unitOfWork.RoomEquipmentWriteRepository.RemoveRange(room.Equipments.ToList());
        }

        // Otağın özünü silin
        _unitOfWork.RoomWriteRepository.Remove(room);

        // Bütün qalan dəyişiklikləri vahid transaksiyada yadda saxlayın
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateRoomAsync(int id, RoomUpdateDto dto)
    {
        // Otağı tərcümələri və avadanlıq əlaqələri ilə birlikdə yükləyin (izlənilən rejimdə)
        var roomToUpdate = await _unitOfWork.RoomReadRepository.GetAll(asNoTracking: false) // Update üçün izləmə vacibdir
            .Include(r => r.RoomTranslations)
            .Include(r => r.Equipments)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (roomToUpdate == null)
        {
            throw new Exception("Otaq tapılmadı.");
        }

        // Əsas otaq məlumatlarını DTO-dan yeniləyin
        roomToUpdate.Category = dto.Category;
        roomToUpdate.Area = dto.Area;
        roomToUpdate.Price = dto.Price;
        roomToUpdate.Member = dto.Member;
        roomToUpdate.YoutubeVideoLink = dto.YoutubeVideoLink;

        // Tərcümələri yeniləmək
        var currentTranslations = roomToUpdate.RoomTranslations.ToList();
        var translationsToDelete = currentTranslations
            .Where(ct => !dto.Translations.Any(dt => dt.Id == ct.Id)) // DTO-da olmayanları sil
            .ToList();
        _unitOfWork.RoomTranslationWriteRepository.RemoveRange(translationsToDelete);

        foreach (var newOrUpdatedTranslationDto in dto.Translations)
        {
            var existingTranslation = currentTranslations.FirstOrDefault(ct => ct.Id == newOrUpdatedTranslationDto.Id);
            if (existingTranslation != null)
            {
                // Mövcud tərcüməni yenilə
                existingTranslation.Service = newOrUpdatedTranslationDto.Service;
                existingTranslation.Description = newOrUpdatedTranslationDto.Description;
                existingTranslation.MiniDescription = newOrUpdatedTranslationDto.MiniDescription;
                existingTranslation.Title = newOrUpdatedTranslationDto.Title;
                existingTranslation.MiniTitle = newOrUpdatedTranslationDto.MiniTitle;
                existingTranslation.Language = newOrUpdatedTranslationDto.Language;
                _unitOfWork.RoomTranslationWriteRepository.Update(existingTranslation); // Update metodu çağırılır
            }
            else
            {
                // Yeni tərcüməni əlavə et
                var newTranslation = new RoomTranslation
                {
                    RoomId = roomToUpdate.Id, // Otağın ID-sini təyin et
                    Service = newOrUpdatedTranslationDto.Service,
                    Description = newOrUpdatedTranslationDto.Description,
                    MiniDescription = newOrUpdatedTranslationDto.MiniDescription,
                    Title = newOrUpdatedTranslationDto.Title,
                    MiniTitle = newOrUpdatedTranslationDto.MiniTitle,
                    Language = newOrUpdatedTranslationDto.Language
                };
                await _unitOfWork.RoomTranslationWriteRepository.CreateAsync(newTranslation); // Yeni tərcümə yaradılır
            }
        }

        // Avadanlıqları yeniləmək
        var currentEquipments = roomToUpdate.Equipments.ToList();
        var roomEquipmentsToDelete = currentEquipments
            .Where(ce => !dto.EquipmentIds.Contains(ce.EquipmentId)) // DTO-da olmayanları sil
            .ToList();
        _unitOfWork.RoomEquipmentWriteRepository.RemoveRange(roomEquipmentsToDelete);

        var newEquipmentIds = dto.EquipmentIds
            .Where(newId => !currentEquipments.Any(ce => ce.EquipmentId == newId)) // Mövcud olmayan yeni ID-ləri əlavə et
            .ToList();

        foreach (var newEquipmentId in newEquipmentIds)
        {
            var newRoomEquipment = new RoomEquipment
            {
                RoomId = roomToUpdate.Id,
                EquipmentId = newEquipmentId
            };
            await _unitOfWork.RoomEquipmentWriteRepository.CreateAsync(newRoomEquipment); // Yeni əlaqə yaradılır
        }

        // Şəkilləri silin
        if (dto.ImageIdsToDelete != null && dto.ImageIdsToDelete.Any())
        {
            foreach (var imageId in dto.ImageIdsToDelete)
            {
                await _imageService.DeleteImageAsync(imageId); // ImageService öz daxilində SaveChangesAsync-i çağırır
            }
        }

        // Yeni şəkilləri yükləyin
        if (dto.NewImageFiles != null && dto.NewImageFiles.Any())
        {
            foreach (var file in dto.NewImageFiles)
            {
                var imageCreateDto = new ImageCreateDto
                {
                    File = file,
                    Entity = ImageEntity.Room,
                    RelatedEntityId = roomToUpdate.Id
                };
                await _imageService.UploadImageAsync(imageCreateDto); // ImageService öz daxilində SaveChangesAsync-i çağırır
            }
        }

        // Otaq obyektindəki dəyişiklikləri və yuxarıdakı RoomTranslation/RoomEquipment dəyişikliklərini yadda saxlayın
        _unitOfWork.RoomWriteRepository.Update(roomToUpdate); // Otağın özünü update et
        await _unitOfWork.SaveChangesAsync();
    }
}