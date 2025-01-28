using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Models.Domain;
using Walks.API.Models.DTOs;

namespace Walks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly WalksDbContext dbContext;

        public WalkRepository(WalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk?> CreateWalkAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk; 
        }

        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            var walk = await dbContext.Walks.Include("Region").Include("Difficulty").FirstOrDefaultAsync(w => w.Id == id);

            //if not found.
            if (walk == null) return null;

            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync(); 
            return walk;



        }

        public Task<List<Walk>> GetAllWalksAsync()
        {
           return dbContext.Walks.Include(w=>w.Region).Include(w=>w.Difficulty).ToListAsync();
        }

        public async Task<Walk?> GetWalkById(Guid id)
        {
           //Find the walk.
           var walk = await dbContext.Walks.Include("Region").Include("Difficulty").FirstOrDefaultAsync(w=>w.Id==id);
           return walk; 

        }

        public async Task<Walk> UpdateWalkAsync(Guid id,  UpdateWalkDto walk)
        {
            //find the walk.
            var existingWalk = await dbContext.Walks.FindAsync(id);

            if (existingWalk == null) return null; 

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKms = walk.LengthInKms;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;

            existingWalk.RegionId = walk.RegionId;
            existingWalk.DifficultyId = walk.DifficultyId;

            await dbContext.SaveChangesAsync();
            return existingWalk;

        }

        



    }
}
