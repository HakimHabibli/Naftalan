using NaftalanHotelSystem.Application.DataTransferObject.Package;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.Abstractions.Services;

public interface IPackageService
{
    Task<List<PackageDto>> GetAllPackageAsync();
    Task<PackageDto> GetPackageByIdAsync(int id);
    Task CreatePackageAsync(PackageCreateDto dto);
    Task UpdatePackageAsync(int id, PackageCreateDto dto);
    Task DeletePackageAsync(int id);
}
