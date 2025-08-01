using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.Abstractions.UnitOfWork;
using NaftalanHotelSystem.Application.DataTransferObject.Child;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Application.Concretes.Services;

public class ChildService : IChildService
{
    private readonly IUnitOfWork _unitOfWork;

    public ChildService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ChildGetDto> CreateChildAsync(ChildCreateDto dto)
    {
   
      
        var child = new Child
        {
            Price = dto.Price,
            AgeRange = dto.AgeRange,
            HasTreatment = dto.HasTreatment,
            
        };

        await _unitOfWork.ChildWriteRepository.CreateAsync(child);
        await _unitOfWork.SaveChangesAsync(); 

        return new ChildGetDto
        {
            Id = child.Id, 
            Price = child.Price,
            AgeRange = child.AgeRange,
            HasTreatment = child.HasTreatment
        };
    }

    public async Task DeleteChildAsync(int id)
    {
        
        var child = await _unitOfWork.ChildWriteRepository.Table
                                     .FirstOrDefaultAsync(c => c.Id == id);

        if (child == null)
        {
           
            throw new Exception($"Child with ID {id} not found for deletion.");
        }

        _unitOfWork.ChildWriteRepository.Remove(child);
        await _unitOfWork.SaveChangesAsync();
    }


    public async Task<List<ChildGetDto>> GetAllChildAsync()
    {
        var children = await _unitOfWork.ChildReadRepository
                                        .Table
                                        .AsNoTracking() 
                                        .ToListAsync();

       
        return children.Select(child => new ChildGetDto
        {
            Id = child.Id,
            Price = child.Price,
            AgeRange = child.AgeRange,
            HasTreatment = child.HasTreatment
        }).ToList();
    }

    public async Task<ChildGetDto> GetByIdChildAsync(int id)
    {
        var child = await _unitOfWork.ChildReadRepository
                                     .Table
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(c => c.Id == id);

        if (child == null)
        {
            throw new Exception($"Child with ID {id} not found.");
        }

        return new ChildGetDto
        {
            Id = child.Id,
            Price = child.Price,
            AgeRange = child.AgeRange,
            HasTreatment = child.HasTreatment
        };
    }

    public async Task<ChildGetDto> UpdateChildAsync(ChildUpdateDto dto)
    {
      
        var child = await _unitOfWork.ChildWriteRepository
                                     .Table
                                     .FirstOrDefaultAsync(c => c.Id == dto.Id);

        if (child == null)
        {
            throw new Exception($"Child with ID {dto.Id} not found for update.");
        }

       
        child.Price = dto.Price;
        child.AgeRange = dto.AgeRange;
        child.HasTreatment = dto.HasTreatment;
       

       
        _unitOfWork.ChildWriteRepository.Update(child);
        await _unitOfWork.SaveChangesAsync();

     
        return new ChildGetDto
        {
            Id = child.Id,
            Price = child.Price,
            AgeRange = child.AgeRange,
            HasTreatment = child.HasTreatment
        };
    }
}
