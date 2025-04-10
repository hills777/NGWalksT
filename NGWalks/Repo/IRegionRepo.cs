using System.Runtime.InteropServices;
using NGWalks.Models.Domain;

namespace NGWalks.Repo
{
    public interface IRegionRepo
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region> CreateAsync(Region region);
        Task<Region?> UpdateAsync(Guid id, Region region); 
        Task<Region?> DeleteAsync(Guid id);
    }
}
