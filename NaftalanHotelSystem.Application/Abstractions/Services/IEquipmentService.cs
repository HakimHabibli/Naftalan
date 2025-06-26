using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Domain.Entites;
using NaftalanHotelSystem.Domain.Enums;
using Org.BouncyCastle.Asn1.Mozilla;

namespace NaftalanHotelSystem.Application.Abstractions.Services;

public interface IEquipmentService 
{
    public Task<List<EquipmentDto>> GetAllEquipmentAsync();
    public Task<EquipmentDto> GetEquipmentByIdAsync(int id, Language? language = Language.Az);
    public Task CreateAsync(EquipmentCreateDto dto);
    public Task DeleteEquipmentAsync(int id);
    public Task UpdateEquipmentAsync(int id,EquipmentUpdateDto equipment);
}
public interface IPackageService
{
    Task<List<PackageDto>> GetAllPackageAsync();
    Task<PackageDto> GetPackageByIdAsync(int id,Language? language = Language.Az);
    Task CreatePackageAsync(PackageCreateDto dto);
    Task UpdatePackageAsync(int id, PackageCreateDto dto);
    Task DeletePackageAsync(int id);
}
