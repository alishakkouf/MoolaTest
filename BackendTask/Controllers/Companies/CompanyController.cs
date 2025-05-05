using AutoMapper;
using BackendTask.Common;
using BackendTask.Domain;
using BackendTask.Domain.Contract.CompanyContract;
using BackendTask.Domain.Models.Company;
using BackendTask.Domain.Models.TasksDomain;
using BackendTask.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BackendTask.API.Controllers.Companies
{
    public class CompanyController(ICurrentUserService currentUserService, ICompanyManager companyManager,
        IMapper mapper, IStringLocalizerFactory factory)
        : BaseApiController(factory)
    {
        private readonly ICompanyManager _companyManager = companyManager;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IMapper _mapper = mapper;

        [HttpGet("Get")]
        //[Authorize(Permissions.Company.View)]
        public async Task<ActionResult<CompanyDomain>> GetById(int id)
        {    
            var data = await _companyManager.GetByIdAsync(id);

            return Ok(data);
        }

        [HttpGet("GetAll")]
        //[Authorize(Permissions.Company.View)]
        public async Task<ActionResult<PagedResultDto<CompanyDomain>>> GetAll([FromQuery] PagedAndSortedRequestDto request)
        {
            var data = await _companyManager.GetAllAsync(request);
            
            return Ok(data);
        }


        [HttpPost("Create")]
        //[Authorize(Permissions.Company.Create)]
        public async Task<ActionResult<CompanyDomain>> Create([FromBody] CreateCompanyDomain input)
        {
            await _companyManager.AddAsync(input);

            return Ok();
        }


        [HttpPut("Update")]
        //[Authorize(Permissions.Company.Update)]
        public async Task<ActionResult<CompanyDomain>> Update([FromBody] CompanyDomain input)
        {
            await _companyManager.UpdateAsync(input);

            return Ok();
        }

        [HttpDelete("Delete")]
        //[Authorize(Permissions.Company.Delete)]
        public async Task<IActionResult> Delete([FromQuery]int id)
        {
            await _companyManager.DeleteAsync(id);

            return Ok();
        }
    }
}


