using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.Services;
using MP.ApiDotNet6.Application.Services.Interfaces;
using MP.ApiDotNet6.Domain.Validations;

namespace MP.ApiDotNet6.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseServices _purchaseServices;

        public PurchaseController(IPurchaseServices purchaseServices)
        {
            _purchaseServices = purchaseServices;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] PurchaseDTO purchaseDTO)
        {
            try
            {
                var result = await _purchaseServices.CreateAsync(purchaseDTO);
                if (result.IsSucess)
                    return Ok(result);
                return BadRequest(result);

            }
            catch (DoMainValidationException ex)
            {
                var result = ResultServices.Fail(ex.Message);
                return BadRequest(result);
            }
        }


        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var result = await _purchaseServices.GetAsync();
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var result = await _purchaseServices.GetByIdAsync(id);
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut]
        public async Task<ActionResult> EditAsync([FromBody] PurchaseDTO purchaseDTO)
        {
            try
            {
                var result = await _purchaseServices.UpdateAsync(purchaseDTO);
                if (result.IsSucess)
                    return Ok(result);
                return BadRequest(result);

            }
            catch (DoMainValidationException ex)
            {
                var result = ResultServices.Fail(ex.Message);
                return BadRequest(result);
            }
        }
            [HttpDelete]
            [Route("{id}")]
            public async Task<ActionResult> RemoveAsync(int id)
            {
                var result = await _purchaseServices.RemoveAsync(id);
                if (result.IsSucess)
                    return Ok(result);
                return BadRequest(result);
        }
    }
}
