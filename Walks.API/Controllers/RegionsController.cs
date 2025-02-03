using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Models.Domain;
using Walks.API.Models.DTOs;
using Walks.API.Repositories;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")] //api/regions - URL
    [ApiController] //model state etc.
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly WalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(WalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions() //IActionResult is an interface.
        {

            //get the data from the db.
            var domainRegions = await regionRepository.GetAllRegionsAsync();

            //mapping.
            var regionsDto = mapper.Map<List<RegionDto>>(domainRegions); 

            return Ok(regionsDto); 
            
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            //find from the database.
            var region =  await regionRepository.GetRegionByIdAsync(id);

            //map it back to the DTO.
            if (region == null)
            {
                return BadRequest("Not found"); 

            }
            else
            {
               
                var regionDto = mapper.Map<RegionDto>(region); 

                return Ok(regionDto);
            }
            

        }

        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] RegionDto regionDto) {

            if (ModelState.IsValid) {
                //read the incoming data.
                //map it back to the domain model.
                //send it to the repo.
                //save the changes.


                var region = new Region
                {
                    Name = regionDto.Name,
                    code = regionDto.code,
                    RegionImageUrl = regionDto.RegionImageUrl
                };

                await regionRepository.CreateRegionAsync(region);
                return CreatedAtAction(nameof(GetRegionById), new { id = region.Id }, regionDto);

            }
            else
            {
                return BadRequest(ModelState);
            }

        
        
        }

        [HttpPost]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] RegionDto regionDto)
        {
            if (ModelState.IsValid) {

                //find region.
                var region = await regionRepository.GetRegionByIdAsync(id);


                //if not found.
                if (region == null)
                {
                    return NotFound("Provided id isn't located in our db");
                }


                //update region.
                var updatedRegion = await regionRepository.UpdateRegionAsync(region, regionDto);

                return Ok(regionDto);
            }

            return BadRequest(ModelState);


        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id) 
         {
            //find the region.
            //delete the region data.
            //save changes.
            var region = await regionRepository.GetRegionByIdAsync(id);

            //validate if the region exists.
            if(region == null)
            {
                return NotFound("Region cannot be located"); 
            }

            //delete the region and update the database.
            await regionRepository.DeleteRegionAsync(region);

            //map it back to the DTO representation.
            var regionDto = mapper.Map<RegionDto>(region);

            return Ok(regionDto);
         }


           
        
    }
}
