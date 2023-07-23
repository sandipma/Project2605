using BaseModels.ApplicationUser;
using BaseRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Project2605.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IapplicationUser _applicationUser;
        private readonly IConfiguration _config;
   
        public ApplicationUserController(IConfiguration config, IapplicationUser applicationUser)
        {
            _config = config;
            _applicationUser = applicationUser;

        }
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(ApplicationUserInsert applicationUserCreate)
        {


            var registeredUser = await _applicationUser.RegisterUser(applicationUserCreate);

            if (registeredUser != null)
            {
                return Ok("User Registerd successfully");
            }

            return BadRequest("Please try again");
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApplicationUser>> Login(ApplicationUserInsert applicationUserCreate)
        {
            var applicationUserIdentity = await _applicationUser.ValidateUser(applicationUserCreate);


            if (applicationUserIdentity != null)
            {
                return Ok(applicationUserIdentity);
            }


            return BadRequest("Kindly enter correct details");
        }
    }
}
