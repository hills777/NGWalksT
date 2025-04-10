using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NGWalks.Data;
using NGWalks.Models.Domain;
using NGWalks.Models.DTO;
using NGWalks.Repo;

namespace NGWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NGWalksDbContext dbContext;
        private readonly IRegionRepo regionRepo;
        private readonly IMapper mapper;

        public RegionsController(NGWalksDbContext dbContext, IRegionRepo regionRepo, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepo = regionRepo;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepo.GetAllAsync();
            /*var regionDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionDto.Add(new RegionDto
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }*/
            //Map Domain model to Dto
           var regionsDto =  mapper.Map<List<RegionDto>>(regionsDomain);

            return Ok(regionsDto);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var regionDomain = await regionRepo.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map to Model to DTo
            /*var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };*/
            var regionDto = mapper.Map<RegionDto>(regionDomain);
            return Ok(regionDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequest)
        {
            //Convert Dto to Domain Model
            var regionDomainM = mapper.Map<Region>(addRegionRequest);

            //Use Dmomain model to create a region
          regionDomainM =  await regionRepo.CreateAsync(regionDomainM);

            //Map to Model back to DTo
            var regionDto = mapper.Map<RegionDto>(regionDomainM);
            return CreatedAtAction(nameof(GetByIdAsync),new {Id = regionDto.Id}, regionDto);

        }
        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateRegionRquestDto updateRegion)
        {
            //Map Dto to model
            var regionDomainModel = mapper.Map<Region>(updateRegion);
           regionDomainModel =  await regionRepo.UpdateAsync(Id, regionDomainModel);
            if (regionDomainModel != null) { return NotFound(); }

            //Convert Model to Dto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);

        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id) 
        { 
            var regionDomainModel = await regionRepo.DeleteAsync(Id);
            if (regionDomainModel == null) { return NotFound(); }

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);  
        
        }


    }
}
