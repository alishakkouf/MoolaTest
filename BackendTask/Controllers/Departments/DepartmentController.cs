using AutoMapper;
using BackendTask.Common;
using BackendTask.Domain.Contract.DepartmentContract;
using BackendTask.Domain;
using BackendTask.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using BackendTask.Domain.Models.Departments;

namespace BackendTask.API.Controllers.Departments
{
    public class DepartmentController(ICurrentUserService currentUserService, IDepartmentManager depManager,
        IMapper mapper, IStringLocalizerFactory factory)
        : BaseApiController(factory)
    {
        private readonly IDepartmentManager _depManager = depManager;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IMapper _mapper = mapper;

        [HttpGet("Get")]
        //[Authorize(Permissions.Department.View)]
        public async Task<ActionResult<DepartmentDomain>> GetById(int id)
        {
            var data = await _depManager.GetByIdAsync(id);

            return Ok(data);
        }

        [HttpGet("GetAll")]
        //[Authorize(Permissions.Department.View)]
        public async Task<ActionResult<PagedResultDto<DepartmentDomain>>> GetAll([FromQuery] PagedAndSortedRequestDto request)
        {
            var data = await _depManager.GetAllAsync(request);

            return Ok(data);
        }


        [HttpPost("Create")]
        //[Authorize(Permissions.Department.Create)]
        public async Task<ActionResult<DepartmentDomain>> Create([FromBody] CreateDepartmentDomain input)
        {
            await _depManager.AddAsync(input);

            return Ok();
        }


        [HttpPut("Update")]
        //[Authorize(Permissions.Department.Update)]
        public async Task<ActionResult<DepartmentDomain>> Update([FromBody] DepartmentDomain input)
        {
            await _depManager.UpdateAsync(input);

            return Ok();
        }

        [HttpDelete("Delete")]
        //[Authorize(Permissions.Department.Delete)]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _depManager.DeleteAsync(id);

            return Ok();
        }
    }
}


