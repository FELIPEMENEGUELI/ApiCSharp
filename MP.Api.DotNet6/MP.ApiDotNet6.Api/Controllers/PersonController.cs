using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.Services.Interfaces;
using MP.ApiDotNet6.Domain.FiltersDb;

namespace MP.ApiDotNet6.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonServices _personServices;

        public PersonController(IPersonServices personServices)
        {
            _personServices = personServices;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] PersonDTO personDTO)
        {
            var result = await _personServices.CreateAsync(personDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var result = await _personServices.GetAsync();
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var result = await _personServices.GetByIdAsync(id);
            if(result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut]
        public async Task<ActionResult> UptadeAsync([FromBody] PersonDTO personDTO)
        {
            var result = await _personServices.UpdateAsync(personDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _personServices.DeleteAsync(id);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("Paged")]
                                                    // usando FromQuery para concatenar a string 
        public async Task<ActionResult> GetPagedAsync([FromQuery] PersonFilterDb personFilterDb) 
        {
            var result = await _personServices.GetPagedAsync(personFilterDb);
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
 