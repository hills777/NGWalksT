using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NGWalks.CustomActionFilters;
using NGWalks.Data;
using NGWalks.Models.Domain;
using NGWalks.Models.DTO;
using NGWalks.Repo;

namespace NGWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepo walkRepo;
        private readonly NGWalksDbContext dbContext;

        public WalksController(IMapper mapper, IWalkRepo walkRepo, NGWalksDbContext dbContext)
        {
            this.mapper = mapper;
            this.walkRepo = walkRepo;
            this.dbContext = dbContext;
        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDto addWalksRequest)
        {
            

            try
            {
                var difficultyExists = await dbContext.Difficulties
        .AnyAsync(d => d.Id == addWalksRequest.DifficultyId);

                if (!difficultyExists)
                {
                    return BadRequest($"Difficulty with ID {addWalksRequest.DifficultyId} does not exist");
                }

                // map DTO to Domain model
                if (addWalksRequest == null || addWalksRequest.DifficultyId == Guid.Empty || addWalksRequest.RegionId == Guid.Empty)
                {
                    return BadRequest("Invalid input data.");
                }
                var walkDomainModel = mapper.Map<Walk>(addWalksRequest);
                Console.WriteLine($"Walk: {walkDomainModel.Name}, {walkDomainModel.LengthInKm}, {walkDomainModel.DifficultyId}, {walkDomainModel.RegionId}");

                await walkRepo.CreateAsync(walkDomainModel);
                // map Domain model to DTO

                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");

                return BadRequest("Error occurred while processing the request.");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var walksDomainModel = await walkRepo.GetAllAsync();

            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));

        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalkBy([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepo.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, UpdateWalksRequestDto updateWalksRequest)
        {
            var walkDomainModel = mapper.Map<Walk>(updateWalksRequest);
            await walkRepo.UpdateAsync(id, walkDomainModel);
            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));



        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepo.DeleteAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
    }
}
