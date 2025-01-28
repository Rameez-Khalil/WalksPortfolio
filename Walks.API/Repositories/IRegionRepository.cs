using Walks.API.Models.Domain;
using Walks.API.Models.DTOs;

namespace Walks.API.Repositories;

public interface IRegionRepository
{
    public Task<List<Region>> GetAllRegionsAsync(); 
    public Task<Region?> GetRegionByIdAsync(Guid regionId);

    public Task<Region> UpdateRegionAsync(Region region, RegionDto regionUpdates); 
    public Task<Region?> DeleteRegionAsync(Region region);
    public Task<Region> CreateRegionAsync(Region region);




}
