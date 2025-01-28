using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Walks.API.Models.Domain;
using Walks.API.Models.DTOs;
using Walks.API.Repositories;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //this is an APi controller and not an MVC.
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto request)
        {
            if (ModelState.IsValid)
            {
                //map it back to the domain model.
                var walkDOmain = mapper.Map<Walk>(request);
                await walkRepository.CreateWalkAsync(walkDOmain);


                //map the domain back to the DTO.

                return Ok(mapper.Map<AddWalkRequestDto>(walkDOmain));
            }

            else
            {
                return BadRequest();
            }
           

        }

        [HttpGet]
        public async Task<IActionResult> GetWalks()
        {
            //find regions through the repo.
            //return the result back.

            var walks = await walkRepository.GetAllWalksAsync();

            //map it back to the DTO.
            
           return Ok(mapper.Map<List<WalkDto>>(walks));

            
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            //pass the id to the repo.
            //get the data back, if not found, immediately drop the request.
            //return the result.

            var walk = await walkRepository.GetWalkById(id);

            //validate if we have found the walk.
            if (walk == null) return NotFound("Walk cannot be found"); 

            return Ok(mapper.Map<WalkDto>(walk));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkDto request)
        {
            if (ModelState.IsValid) 
            {
                ///based off of the id find the walk.
                ///update the walk with the provided details.
                //return the result.

                var walkSaved = await walkRepository.UpdateWalkAsync(id, request);
                if (walkSaved == null) return NotFound("Walk cannot be located");

                return Ok(mapper.Map<UpdateWalkDto>(walkSaved));

            }
            else
            {
                return BadRequest();
            }


        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            //send the id to the repo.
            //find the walk asasociated with the id.
            //if the walk is identified then delete it.
            //if not found then return null.


            var walkDeleted = await walkRepository.DeleteWalkAsync(id);

            if (walkDeleted == null) return NotFound("Cannot locate the walk");

            return Ok(mapper.Map<WalkDto>(walkDeleted)); 
        }
       
    }
}
