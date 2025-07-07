using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.Abstractions.UnitOfWork;
using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Infrastructure.Services;

public class RoomService : IRoomService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoomService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateRoomAsync(RoomCreateDto dto)
    {
        var room = new Room
        {
            Area = dto.Area,
            Category = dto.Category,
            Member = dto.Member,
            Picture = dto.Picture,
            Price = dto.Price,
            YoutubeVideoLink = dto.YoutubeVideoLink,
            RoomTranslations = dto.Translations.Select
            (
                t => new RoomTranslation
                {
                    Service = t.Service,
                    Description = t.Description,
                    MiniDescription = t.MiniDescription,
                    MiniTitle = t.MiniTitle,
                    Title = t.Title,
                    Language = t.Language
                }
            ).ToList(),

            Equipments = dto.EquipmentIds.Select(
            id => new RoomEquipment { EquipmentId = id }
            ).ToList()
        };

      
        await _unitOfWork.RoomWriteRepository.CreateAsync(room);
        await _unitOfWork.SaveChangesAsync();
        
    }

    public async Task DeleteRoomAsync(int id)
    {
        var room = await _unitOfWork.RoomReadRepository.Table.FirstOrDefaultAsync(r => r.Id == id);

         _unitOfWork.RoomWriteRepository.Table.Remove(room);

        await _unitOfWork.SaveChangesAsync();
    }


    public async Task<List<RoomGetDto>> GetAllRoomsAsync()
    {
        var rooms = await _unitOfWork.RoomReadRepository.Table
            .Include(r => r.RoomTranslations)
            .Include(e => e.Equipments)
            .ToListAsync();

        var result = rooms.Select(room => new RoomGetDto
        {
            Id = room.Id,
            Category = room.Category,
            Area = room.Area,
            Price = room.Price,
            Member = room.Member,
            Picture = room.Picture,
            YoutubeVideoLink = room.YoutubeVideoLink,
            Translations = room.RoomTranslations.Select(t => new RoomTranslationCreateDto
            {
                Service = t.Service,
                Description = t.Description,
                MiniDescription = t.MiniDescription,
                MiniTitle = t.MiniTitle,
                Title = t.Title,
                Language = t.Language
            }).ToList(),
            EquipmentIds = room.Equipments.Select(re => re.EquipmentId).ToList()
        }).ToList();

        return result;
    }


    public async Task<RoomCreateDto> GetRoomByIdAsync(int id)
    {
        var room = await _unitOfWork.RoomReadRepository.Table
            .Include(r => r.RoomTranslations)
            .Include(r => r.Equipments)
                .ThenInclude(re => re.Equipment)
            .FirstOrDefaultAsync(r => r.Id == id);

    

        var dto = new RoomCreateDto
        {
            Category = room.Category,
            Area = room.Area,
            Price = room.Price,
            Member = room.Member,
            Picture = room.Picture,
            YoutubeVideoLink = room.YoutubeVideoLink,
            Translations = room.RoomTranslations.Select(t => new RoomTranslationCreateDto
            {
                Service = t.Service,
                Description = t.Description,
                MiniDescription= t.MiniDescription,
                MiniTitle = t.MiniTitle,
                Title = t.Title,
                Language = t.Language
            }).ToList(),
            EquipmentIds = room.Equipments.Select(re => re.EquipmentId).ToList()
        };

        return dto;
    }


    public async Task UpdateRoomAsync(int id, RoomCreateDto dto)
    {
        var room = await _unitOfWork.RoomReadRepository.Table
            .Include(r => r.RoomTranslations)
            .Include(r => r.Equipments)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (room == null)
            throw new Exception("Room not found");

        // əsas məlumatları yenilə
        room.Category = dto.Category;
        room.Area = dto.Area;
        room.Member = dto.Member;
        room.Picture = dto.Picture;
        room.Price = dto.Price;
        room.YoutubeVideoLink = dto.YoutubeVideoLink;

        // translation-ları yenilə
        room.RoomTranslations = dto.Translations.Select(t => new RoomTranslation
        {
            Service = t.Service,
            Description = t.Description,
            MiniDescription = t.MiniDescription,
            MiniTitle= t.MiniTitle,
            Title = t.Title,
            Language = t.Language
        }).ToList();

        room.Equipments = dto.EquipmentIds.Select(eId => new RoomEquipment
        {
            EquipmentId = eId,
            RoomId = room.Id
        }).ToList();

        _unitOfWork.RoomWriteRepository.Update(room);
        await _unitOfWork.SaveChangesAsync();
    }

}
