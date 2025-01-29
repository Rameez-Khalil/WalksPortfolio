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

        public async Task<List<Walk>> GetAllWalksAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending, int pageNumber, int pageSize)
        {
            //filter.
            var walks =  dbContext.Walks.Include("Region").Include("Difficulty").AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn)==false && string.IsNullOrWhiteSpace(filterQuery)==false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    walks = walks.Where(w => w.Name.ToLower().Contains(filterQuery.ToLower())); 
            }

            //sorting.
            //check if the provided sorting keywords have some values.
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                //check for the name and length in kms.
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(w => w.Name) : walks.OrderByDescending(w=>w.Name); 
                }
                else if(sortBy.Equals("LengthInKms", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(w => w.LengthInKms) : walks.OrderByDescending(w => w.LengthInKms); 
                }
            }


            //pagination.
            var skipResults = (pageNumber - 1) * pageSize;



            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();


           //return dbContext.Walks.Include(w=>w.Region).Include(w=>w.Difficulty).ToListAsync();
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
