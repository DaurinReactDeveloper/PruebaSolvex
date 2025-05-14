using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaSolvex.Application.Contract;
using PruebaSolvex.Application.Dtos.UserDto;
using PruebaSolvex.Application.Servicies;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaSolvex.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserServices _userServices;
        private readonly IConfiguration _configuration;

        public UserController(IUserServices userServices, IConfiguration configuration)
        {

            this._userServices = userServices;
            this._configuration = configuration;

        }


        // GET: api/<UserController>
        [HttpGet("GetUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {

            var result = await _userServices.GetUsers();

            if (!result.Success)
            {

                return BadRequest(result);

            }

            return Ok(result);

        }

        // GET api/<UserController>/5
        [HttpGet("GetUser/{email}/{password}")]
        public async Task<IActionResult> GetUser(string email, string password)
        {

            var result = await _userServices.GetUser(email, password);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            var token = JwtServices.GenerateToken(result.Data.Name, result.Data.Role, _configuration);

            return Ok(new
            {
                Data = result,
                Token = token
            });

        }

        // POST api/<UserController>
        [HttpPost("Save")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] UserAddDto userAdd)
        {
            var result = await _userServices.Add(userAdd);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }


        // PUT api/<UserController>/5
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromBody] UserUpdateDto userUpdate)
        {

            var result = await _userServices.Update(userUpdate);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        // DELETE api/<UserController>/5
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromBody] UserRemoveDto userRemove)
        {

            var result = await _userServices.Remove(userRemove);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }



    }
}
