using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.Services.Interfaces;

namespace MP.ApiDotNet6.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpPost]
        public async Task <ActionResult> PostAsync([FromBody] ProductDTO productDTO)
        {
            var result = await _productServices.CreateAsync(productDTO);
            if(result.IsSucess)
                    return Ok(result);
            return BadRequest(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var result = await _productServices.GetAsync();
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var result = await _productServices.GetByIdAsync(id);
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] ProductDTO productDTO)
        {
            var result = await _productServices.UpdateAsync(productDTO);
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _productServices.DeleteAsync(id);
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
