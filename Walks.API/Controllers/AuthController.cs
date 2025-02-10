using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Walks.API.Models.DTOs;
using Walks.API.Repositories;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepo tokenRepo;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepo tokenRepo)
        {
            this.userManager = userManager;
            this.tokenRepo = tokenRepo;
        }

        [HttpPost] //POST /api/auth/register.
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, request.Password);


            //if user successfully created.

            if (identityResult.Succeeded)
            {
                //assign roles.
                if (request.Roles != null && request.Roles.Any())
                {
                   identityResult =  await userManager.AddToRolesAsync(identityUser, request.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User registered"); 
                    }
                
                }

            }

            return BadRequest("Something went wrong"); 
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            //find the user.
            var user = await userManager.FindByEmailAsync(loginRequest.UserName);


            if (user != null) {

                //check for the password.
                var checkPassword = await userManager.CheckPasswordAsync(user, loginRequest.Password);

               

                //generate token.
                if (checkPassword)
                {
                    //get roles.
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var token = tokenRepo.CreateJwtToken(user, roles.ToList());

                        var tokenResponse = new LoginResponseDto
                        {
                            Token = token
                        }; 
                        return Ok(tokenResponse);
                    }
                       
                }

            }

            return BadRequest("Incorrect user or password field"); 

        }


    }
}
