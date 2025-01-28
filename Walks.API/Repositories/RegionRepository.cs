using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Models.Domain;
using Walks.API.Models.DTOs;

namespace Walks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly WalksDbContext dbContext;

        public RegionRepository(WalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<Region> CreateRegionAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteRegionAsync(Region region)
        {
            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();
            return region; 
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            //get the data from the db.
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionByIdAsync(Guid regionId)
        {
            return await dbContext.Regions.FindAsync(regionId);
        }

       

        public async Task<Region> UpdateRegionAsync(Region region, RegionDto regionUpdates)
        {
            //update region.
            region.Name = regionUpdates.Name;
            region.code = regionUpdates.code;
            region.RegionImageUrl = regionUpdates.RegionImageUrl;

            //call the db to update the changes.
            await dbContext.SaveChangesAsync();
            return region;
        }
    }
}
