using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.Abstractions.UnitOfWork;
using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Application.DataTransferObject.Child;
using NaftalanHotelSystem.Application.DataTransferObject.Image;
using NaftalanHotelSystem.Application.DataTransferObject.Room;
using NaftalanHotelSystem.Domain.Entites;
using NaftalanHotelSystem.Domain.Enums;

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
        var rooms = await _unitOfWork.RoomReadRepository.GetAll(asNoTracking: true)
            .Include(r => r.RoomTranslations)
            .Include(r => r.Equipments)
            .Include(r => r.RoomChildren)
                .ThenInclude(rc => rc.Child)
            .Include(r=>r.RoomPricesByOccupancy)
            .ToListAsync();

        var roomDtos = new List<RoomGetDto>();

        foreach (var room in rooms)
        {
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
                    Id = t.Id,
                    Service = t.Service,
                    Description = t.Description,
                    MiniDescription = t.MiniDescription,
                    Title = t.Title,
                    MiniTitle = t.MiniTitle,
                    Language = t.Language
                }).ToList(),
                Children = room.RoomChildren.Select(rc => new ChildGetDto
                {
                    Id = rc.Child.Id,
                    AgeRange = rc.Child.AgeRange,
                    HasTreatment = rc.Child.HasTreatment,
                    Price = rc.Child.Price,
                }).ToList(),
                EquipmentIds = room.Equipments.Select(re => re.EquipmentId).ToList(),
                ImageUrls = roomImages.Select(img => img.Url).ToList(),
                PricesByOccupancy = room.RoomPricesByOccupancy.Select(p=> new RoomPriceByOccupancyDto
                {
                    Occupancy = p.Occupancy,
                    Price = p.Price
                }).ToList()
            });
        }

        return roomDtos;
    }

    public async Task<RoomGetDto> GetRoomByIdAsync(int id)
    {
        var room = await _unitOfWork.RoomReadRepository.GetAll(asNoTracking: true)
            .Include(r => r.RoomTranslations)
            .Include(r => r.Equipments)
            .Include(r => r.RoomChildren).ThenInclude(rc => rc.Child)
            .Include(r=>r.RoomPricesByOccupancy)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (room == null)
            throw new Exception($"Room with ID {id} not found.");

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

            Children = room.RoomChildren.Select(rc => new ChildGetDto
            {
                Id = rc.Child.Id,
                AgeRange = rc.Child.AgeRange,
                HasTreatment = rc.Child.HasTreatment,
                Price = rc.Child.Price
            }).ToList(),
            PricesByOccupancy = room.RoomPricesByOccupancy.Select(e => new RoomPriceByOccupancyDto
            {
                Occupancy = e.Occupancy,
                Price = e.Price
            }).ToList(),
            EquipmentIds = room.Equipments.Select(e => e.EquipmentId).ToList(),
            ImageUrls = roomImages.Select(img => img.Url).ToList()
        };
    }


    public async Task CreateRoomAsync(RoomCreateDto dto)
    {
        var room = new Room
        {
            Category = dto.Category,
            Area = dto.Area,
            Member = dto.Member,
            Price = dto.Price,
            YoutubeVideoLink = dto.YoutubeVideoLink,
            RoomPricesByOccupancy = dto.PricesByOccupancy?.Select(p => new RoomPriceByOccupancy
            {
                Occupancy = p.Occupancy,
                Price = p.Price
            }).ToList() ?? new List<RoomPriceByOccupancy>(),
            RoomTranslations = dto.Translations?.Select(t => new RoomTranslation
            {
                Service = t.Service,
                Description = t.Description,
                MiniDescription = t.MiniDescription,
                MiniTitle = t.MiniTitle,
                Title = t.Title,
                Language = t.Language
            }).ToList() ?? new List<RoomTranslation>()
            
        };

        
        if (dto.EquipmentIds != null && dto.EquipmentIds.Any())
        {
            var existingEquipmentIds = await _unitOfWork.EquipmentReadRepository
                .Table
                .Where(e => dto.EquipmentIds.Contains(e.Id))
                .Select(e => e.Id)
                .ToListAsync();

            var invalidEquipmentIds = dto.EquipmentIds.Except(existingEquipmentIds).ToList();
            if (invalidEquipmentIds.Any())
                throw new ArgumentException($"Mövcud olmayan Avadanlıq ID-ləri: {string.Join(", ", invalidEquipmentIds)}");

            room.Equipments = existingEquipmentIds.Select(id => new RoomEquipment
            {
                EquipmentId = id
            }).ToList();
        }

        if (dto.ChildIds != null && dto.ChildIds.Any())
        {
            var existingChildren = await _unitOfWork.ChildReadRepository
                .Table
                .Where(c => dto.ChildIds.Contains(c.Id))
                .ToListAsync();

            var invalidChildIds = dto.ChildIds.Except(existingChildren.Select(c => c.Id)).ToList();
            if (invalidChildIds.Any())
                throw new ArgumentException($"Mövcud olmayan Uşaq ID-ləri: {string.Join(", ", invalidChildIds)}");

            room.RoomChildren = existingChildren.Select(child => new RoomChild
            {
                ChildId = child.Id
            }).ToList();
        }

        await _unitOfWork.RoomWriteRepository.CreateAsync(room);
        await _unitOfWork.SaveChangesAsync();

       
        if (dto.ImageFiles != null && dto.ImageFiles.Any())
        {
            foreach (var file in dto.ImageFiles)
            {
                var imageCreateDto = new ImageCreateDto
                {
                    File = file,
                    Entity = ImageEntity.Room,
                    RelatedEntityId = room.Id 
                };
                await _imageService.UploadImageAsync(imageCreateDto);
            }
        }
    }





    public async Task DeleteRoomAsync(int id)
    {
        var room = await _unitOfWork.RoomReadRepository.GetAll(asNoTracking: false)
            .Include(r => r.RoomTranslations)
            .Include(r => r.Equipments)
            .Include(r => r.RoomChildren)
            .Include(r => r.RoomPricesByOccupancy)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (room == null)
            throw new Exception("Otaq tapılmadı.");

        var imagesToDelete = await _unitOfWork.ImageReadRepository.GetAll(asNoTracking: true)
            .Where(x => x.Entity == ImageEntity.Room && x.RelatedEntityId == room.Id)
            .ToListAsync();

        foreach (var image in imagesToDelete)
        {
            await _imageService.DeleteImageAsync(image.Id);
        }


        if (room.RoomTranslations != null && room.RoomTranslations.Any())
            _unitOfWork.RoomTranslationWriteRepository.RemoveRange(room.RoomTranslations.ToList());

        if (room.Equipments != null && room.Equipments.Any())
            _unitOfWork.RoomEquipmentWriteRepository.RemoveRange(room.Equipments.ToList());

        if (room.RoomChildren != null && room.RoomChildren.Any())
            _unitOfWork.RoomChildWriteRepository.RemoveRange(room.RoomChildren.ToList());

        if (room.RoomPricesByOccupancy != null && room.RoomPricesByOccupancy.Any())
            _unitOfWork.RoomPriceByOccupancyWriteRepository.RemoveRange(room.RoomPricesByOccupancy.ToList());


        _unitOfWork.RoomWriteRepository.Remove(room);
        await _unitOfWork.SaveChangesAsync();
    }


    public async Task UpdateRoomAsync(int id, RoomUpdateDto dto)
    {
        var roomToUpdate = await _unitOfWork.RoomReadRepository.GetAll(asNoTracking: false)
            .Include(r => r.RoomTranslations)
            .Include(r => r.Equipments)
            .Include(r => r.RoomChildren)
            .Include(r=>r.RoomPricesByOccupancy)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (roomToUpdate == null)
            throw new Exception("Otaq tapılmadı.");

        roomToUpdate.Category = dto.Category;
        roomToUpdate.Area = dto.Area;
        roomToUpdate.Price = dto.Price;
        roomToUpdate.Member = dto.Member;
        roomToUpdate.YoutubeVideoLink = dto.YoutubeVideoLink;

        var currentTranslations = roomToUpdate.RoomTranslations.ToList();
        var translationsToDelete = currentTranslations.Where(ct => !dto.Translations.Any(dt => dt.Id == ct.Id)).ToList();
        _unitOfWork.RoomTranslationWriteRepository.RemoveRange(translationsToDelete);

        foreach (var translationDto in dto.Translations)
        {
            var existingTranslation = currentTranslations.FirstOrDefault(ct => ct.Id == translationDto.Id);
            if (existingTranslation != null)
            {
                existingTranslation.Service = translationDto.Service;
                existingTranslation.Description = translationDto.Description;
                existingTranslation.MiniDescription = translationDto.MiniDescription;
                existingTranslation.Title = translationDto.Title;
                existingTranslation.MiniTitle = translationDto.MiniTitle;
                existingTranslation.Language = translationDto.Language;
                _unitOfWork.RoomTranslationWriteRepository.Update(existingTranslation);
            }
            else
            {
                var newTranslation = new RoomTranslation
                {
                    RoomId = roomToUpdate.Id,
                    Service = translationDto.Service,
                    Description = translationDto.Description,
                    MiniDescription = translationDto.MiniDescription,
                    Title = translationDto.Title,
                    MiniTitle = translationDto.MiniTitle,
                    Language = translationDto.Language
                };
                await _unitOfWork.RoomTranslationWriteRepository.CreateAsync(newTranslation);
            }
        }

       
        var currentEquipments = roomToUpdate.Equipments.ToList();
        var equipmentsToDelete = currentEquipments.Where(ce => !dto.EquipmentIds.Contains(ce.EquipmentId)).ToList();
        _unitOfWork.RoomEquipmentWriteRepository.RemoveRange(equipmentsToDelete);

        var equipmentsToAdd = dto.EquipmentIds.Where(newId => !currentEquipments.Any(ce => ce.EquipmentId == newId)).ToList();
        foreach (var newEquipmentId in equipmentsToAdd)
        {
            var newRoomEquipment = new RoomEquipment
            {
                RoomId = roomToUpdate.Id,
                EquipmentId = newEquipmentId
            };
            await _unitOfWork.RoomEquipmentWriteRepository.CreateAsync(newRoomEquipment);
        }

       
        if (dto.NewImageFiles != null && dto.NewImageFiles.Any())
        {
            var existingImages = await _unitOfWork.ImageReadRepository.GetAll(asNoTracking: true)
                .Where(x => x.Entity == ImageEntity.Room && x.RelatedEntityId == roomToUpdate.Id)
                .ToListAsync();

            foreach (var img in existingImages)
            {
                await _imageService.DeleteImageAsync(img.Id);
            }

            foreach (var file in dto.NewImageFiles)
            {
                var imageCreateDto = new ImageCreateDto
                {
                    File = file,
                    Entity = ImageEntity.Room,
                    RelatedEntityId = roomToUpdate.Id
                };
                await _imageService.UploadImageAsync(imageCreateDto);
            }
        }

        
        var existingRoomChildren = roomToUpdate.RoomChildren.ToList();
        _unitOfWork.RoomChildWriteRepository.RemoveRange(existingRoomChildren);

        
        if (dto.ChildIds != null && dto.ChildIds.Any())
        {
            var childrenToAdd = await _unitOfWork.ChildReadRepository
                .Table
                .Where(c => dto.ChildIds.Contains(c.Id))
                .ToListAsync();

            roomToUpdate.RoomChildren = childrenToAdd.Select(child => new RoomChild
            {
                RoomId = roomToUpdate.Id,
                ChildId = child.Id
            }).ToList();
        }
        _unitOfWork.RoomPriceByOccupancyWriteRepository.RemoveRange(roomToUpdate.RoomPricesByOccupancy);

        // Yeniləri əlavə edirik
        if (dto.PricesByOccupancy != null && dto.PricesByOccupancy.Any())
        {
            foreach (var priceDto in dto.PricesByOccupancy)
            {
                var newPrice = new RoomPriceByOccupancy
                {
                    RoomId = roomToUpdate.Id,
                    Occupancy = priceDto.Occupancy,
                    Price = priceDto.Price
                };
                await _unitOfWork.RoomPriceByOccupancyWriteRepository.CreateAsync(newPrice);
            }
        }


        _unitOfWork.RoomWriteRepository.Update(roomToUpdate);
        await _unitOfWork.SaveChangesAsync();
    }
}