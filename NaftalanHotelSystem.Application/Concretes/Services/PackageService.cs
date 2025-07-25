﻿using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.Abstractions.UnitOfWork;
using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Application.DataTransferObject.Package;
using NaftalanHotelSystem.Domain.Entites;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.Concretes.Services;

public class PackageService : IPackageService
{
    private readonly IUnitOfWork _unitOfWork;

    public PackageService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<List<PackageDto>> GetAllPackageAsync()
    {
        var packages = await _unitOfWork.PackageReadRepository.Table
            .Include(p => p.PackageTranslations)
            .Include(p => p.TreatmentMethods)
                .ThenInclude(tm => tm.Translations)
            .ToListAsync();

        var result = packages.Select(p => new PackageDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            DurationDay = p.DurationDay,
            RoomType = p.RoomType,
            PackageTranslations = p.PackageTranslations.Select(t => new PackageTranslationDto
            {
                Description = t.Description,
                Language = t.Language 
            }).ToList(),
            TreatmentMethods = p.TreatmentMethods
            .SelectMany(tm => tm.Translations.Select(tr => new TreatmentMethodDto
            {
                Id = tm.Id,
                Name = tr.Name,
                Description = tr.Description,
                Language = tr.Language
            })).ToList()
        }).ToList();

        return result;
    }



    public async Task<PackageDto> GetPackageByIdAsync(int id)
    {
        var package = await _unitOfWork.PackageReadRepository.Table
            .Include(p => p.PackageTranslations)
            .Include(p => p.TreatmentMethods)
                .ThenInclude(tm => tm.Translations)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (package == null)
            return null; 

  
        var packageTranslations = package.PackageTranslations.Select(pt => new PackageTranslationDto
        {
            Description = pt.Description,
            Language = pt.Language
        }).ToList();

        var treatmentMethods = package.TreatmentMethods.Select(tm =>
        {
    
            var tmTranslations = tm.Translations.Select(t => new TreatmentMethodTranslationGetByIdDto
            {
                Name = t.Name,
                Description = t.Description,
                Language = t.Language
            }).ToList();

   
            var selectedTmTranslation = tmTranslations.FirstOrDefault(t => t.Language == Language.Az) ?? tmTranslations.FirstOrDefault();

            return new TreatmentMethodDto
            {
                Id = tm.Id,
                Name = selectedTmTranslation?.Name ?? string.Empty,
                Description = selectedTmTranslation?.Description ?? string.Empty,
                Language = selectedTmTranslation?.Language ?? Language.Az 
            };
        }).ToList();

        return new PackageDto
        {
            Id = package.Id,
            Name = package.Name,
            Price = package.Price,
            DurationDay = package.DurationDay,
            RoomType = package.RoomType,
            PackageTranslations = packageTranslations,
            TreatmentMethods = treatmentMethods
        };
    }




    public async Task CreatePackageAsync(PackageCreateDto dto)
    {
        var treatmentMethods = await _unitOfWork.TreatmentMethodReadRepository.Table
            .Where(tm => dto.TreatmentMethodsIds.Contains(tm.Id))
            .ToListAsync();

        var package = new Package
        {
            Name = dto.Name,
            Price = dto.Price,
            DurationDay = dto.DurationDay,
            RoomType = dto.RoomType,
            PackageTranslations = dto.PackageTranslations.Select(t => new PackageTranslation
            {
                Description = t.Description,Language = t.Language
            }).ToList(),
            TreatmentMethods = treatmentMethods
        };

        await _unitOfWork.PackageWriteRepository.CreateAsync(package);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdatePackageAsync(int id, PackageCreateDto dto)
    {
        var package = await _unitOfWork.PackageWriteRepository.Table
            .Include(p => p.PackageTranslations)
            .Include(p => p.TreatmentMethods)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (package == null) return;

        package.Name = dto.Name;
        package.Price = dto.Price;
        package.DurationDay = dto.DurationDay;
        package.RoomType = dto.RoomType;

        package.PackageTranslations = dto.PackageTranslations.Select(t => new PackageTranslation
        {
            Description = t.Description
        }).ToList();

        package.TreatmentMethods = await _unitOfWork.TreatmentMethodReadRepository.Table
            .Where(tm => dto.TreatmentMethodsIds.Contains(tm.Id))
            .ToListAsync();

        _unitOfWork.PackageWriteRepository.Update(package);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeletePackageAsync(int id)
    {
        var package = await _unitOfWork.PackageWriteRepository.Table
            .Include(p => p.PackageTranslations)
            .Include(p => p.TreatmentMethods)
            .FirstOrDefaultAsync(p => p.Id == id);

        _unitOfWork.PackageWriteRepository.Remove(package);
        await _unitOfWork.SaveChangesAsync();
    }
}
