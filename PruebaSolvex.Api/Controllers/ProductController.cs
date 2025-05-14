using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaSolvex.Application.Contract;
using PruebaSolvex.Application.Dtos.ProductDto;
using PruebaSolvex.Application.Dtos.UserDto;
using PruebaSolvex.Application.Servicies;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaSolvex.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {


        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {

            this._productServices = productServices;

        }

        // GET: api/<ProductController>
        [HttpGet("GetProducts")]
        [Authorize(Roles = "Seller,User,Admin")]
        public async Task<IActionResult> GetUsers()
        {

            var result = await _productServices.GetProducts();

            if (!result.Success)
            {

                return BadRequest(result);

            }

            return Ok(result);

        }

        // GET: api/<ProductController>
        [HttpGet("Search")]
        [Authorize(Roles = "Seller,User,Admin")]
        public async Task<IActionResult> SearchProductsByName([FromQuery] string name)
        {
            var result = await _productServices.SearchProductsByName(name);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }



        // POST api/<ProductController>
        [HttpPost("Save")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> Post([FromBody] ProductAddDto productAdd)
        {
            var result = await _productServices.Add(productAdd);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        // PUT api/<ProductController>/5
        [HttpPut("Update")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> Put([FromBody] ProductUpdateDto productUpdate)
        {

            var result = await _productServices.Update(productUpdate);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        // DELETE api/<ProductController>/5
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromBody] ProductRemoveDto productRemove)
        {

            var result = await _productServices.Remove(productRemove);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

    }
}
