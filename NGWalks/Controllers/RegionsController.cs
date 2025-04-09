using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NGWalks.Data;
using NGWalks.Models.Domain;
using NGWalks.Models.DTO;

namespace NGWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NGWalksDbContext dbContext;

        public RegionsController(NGWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var regionsDomain = dbContext.Regions.ToList();
            var regionDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionDto.Add(new RegionDto
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

            return Ok(regionsDomain);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map to Model to DTo
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            return Ok(regionDto);
        }
        [HttpPost]

        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequest)
        {
            //Convert Dto to Domain Model
            var regionDomainM = new Region
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                RegionImageUrl = addRegionRequest.RegionImageUrl
            };

            //Use Dmomain model to create a region
            dbContext.Regions.Add(regionDomainM);
            dbContext.SaveChanges();

            //Map to Model back to DTo
            var regionDto = new RegionDto
            {
                Id = regionDomainM.Id,
                Code = regionDomainM.Code,
                Name = regionDomainM.Name,
                RegionImageUrl = regionDomainM.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), regionDto);

        }
        [HttpPut]
        [Route("{Id:Guid}")]
        public IActionResult Update([FromRoute] Guid Id, [FromBody] UpdateRegionRquestDto updateRegion)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == Id);
            if (regionDomainModel == null) {

                return NotFound();
            }
            //Map
            regionDomainModel.Code = updateRegion.Code;
            regionDomainModel.Name = updateRegion.Name;
            regionDomainModel.RegionImageUrl = updateRegion.RegionImageUrl;
            dbContext.SaveChanges();

            //Convert Model to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);

        }

        [HttpDelete]
        [Route("Id:Guid")]
        public IActionResult Delete([FromRoute] Guid Id) 
        { 
            var regionDomainModel = dbContext.Regions.FirstOrDefault( x => x.Id == Id);
            if (regionDomainModel == null) { return NotFound(); }

            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();

            return Ok();  
        
        }


    }
}
