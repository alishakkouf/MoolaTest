using AutoMapper;
using BackendTask.Common;
using BackendTask.Domain.Contract.DepartmentContract;
using BackendTask.Domain.Models.Departments;
using BackendTask.Domain;
using BackendTask.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using BackendTask.Domain.Contract.TransactionsContract;
using BackendTask.Domain.Models.Transactions;
using System.ComponentModel.DataAnnotations;

namespace BackendTask.API.Controllers.Transactions
{
    public class TransactionController(ICurrentUserService currentUserService, ITransactionsManager transManager,
        IMapper mapper, IStringLocalizerFactory factory)
        : BaseApiController(factory)
    {
        private readonly ITransactionsManager _transManager = transManager;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IMapper _mapper = mapper;

        [HttpGet("Get")]
        //[Authorize(Permissions.Transaction.View)]
        public async Task<ActionResult<TransactionDomain>> GetById(int id)
        {
            var data = await _transManager.GetByIdAsync(id);

            return Ok(data);
        }

        [HttpGet("GetUserTransactionHistory")]
        //[Authorize(Permissions.Transaction.View)]
        public async Task<ActionResult<List<TransactionDomain>>> GetUserTransactionHistory([FromQuery] [Required] long id)
        {
            var data = await _transManager.GetUserTransactionHistoryAsync(id);

            return Ok(data);
        }

        [HttpGet("GetAll")]
        //[Authorize(Permissions.Transaction.View)]
        public async Task<ActionResult<PagedResultDto<TransactionDomain>>> GetAll([FromQuery] PagedAndSortedRequestDto request)
        {
            var data = await _transManager.GetAllAsync(request);

            return Ok(data);
        }


        [HttpPost("Create")]
        //[Authorize(Permissions.Transaction.Create)]
        public async Task<ActionResult<TransactionDomain>> Create([FromBody] CreateTransactionDomain input)
        {
            await _transManager.AddAsync(input);

            return Ok();
        }


        [HttpPut("Update")]
        //[Authorize(Permissions.Transaction.Update)]
        public async Task<ActionResult<TransactionDomain>> Update([FromBody] TransactionDomain input)
        {
            await _transManager.UpdateAsync(input);

            return Ok();
        }

        [HttpDelete("Delete")]
        //[Authorize(Permissions.Transaction.Delete)]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _transManager.DeleteAsync(id);

            return Ok();
        }
    }
}



