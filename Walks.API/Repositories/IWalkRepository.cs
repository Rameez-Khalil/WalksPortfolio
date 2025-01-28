using Microsoft.AspNetCore.Mvc;
using Walks.API.Models.Domain;
using Walks.API.Models.DTOs;

namespace Walks.API.Repositories
{
    public interface IWalkRepository
    {
        public Task<Walk> CreateWalkAsync(Walk walk); 
        public Task<List<Walk> > GetAllWalksAsync();
        public Task<Walk> GetWalkById(Guid id);

        public Task<Walk> UpdateWalkAsync(Guid id, UpdateWalkDto walk);
        public Task<Walk> DeleteWalkAsync(Guid id);
    }
}
