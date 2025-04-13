using Microsoft.EntityFrameworkCore;
using NGWalks.Data;
using NGWalks.Models.Domain;

namespace NGWalks.Repo
{
    public class WalkRepo : IWalkRepo
    {
        private readonly NGWalksDbContext dbContext;

        public WalkRepo(NGWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);

            await dbContext.SaveChangesAsync();
            return walk;

        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {

            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.id == id);
            if (existingWalk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var exsistingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.id == id);
            if (exsistingWalk == null)
            {
                return null;
            }
            exsistingWalk.Name = walk.Name;
            exsistingWalk.LengthInKm = walk.LengthInKm;
            exsistingWalk.RegionId = walk.RegionId;
            exsistingWalk.DifficultyId = walk.DifficultyId;
            exsistingWalk.Description = walk.Description;
            exsistingWalk.WalkImageUrl = walk.WalkImageUrl;
            await dbContext.SaveChangesAsync();
            return exsistingWalk;
        }
    }
}
