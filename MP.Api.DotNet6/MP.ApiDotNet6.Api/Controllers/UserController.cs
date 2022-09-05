using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.Services.Interfaces;

namespace MP.ApiDotNet6.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        [Route("token")]
        public async Task<ActionResult> PostAsync([FromForm] UserDTO userDTO)
        {
            var result = await _userServices.GenerateTokenAsync(userDTO);
            if (result.IsSucess)
                return Ok(result.Data);

            return BadRequest(result);
        }
    }
}
