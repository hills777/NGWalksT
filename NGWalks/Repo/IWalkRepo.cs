﻿using NGWalks.Models.Domain;

namespace NGWalks.Repo
{
    public interface IWalkRepo
    {
       Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(); 
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
