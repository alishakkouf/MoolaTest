using AutoMapper;
using BackendTask.Common;
using BackendTask.Domain.Contract.DepartmentContract;
using BackendTask.Domain.Models.Departments;
using BackendTask.Domain;
using BackendTask.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using BackendTask.Domain.Contract.BankCardContract;
using BackendTask.Domain.Models.BankCards;
using BackendTask.API.Controllers.BankCards.Dtos;
using BackendTask.Domain.Contract.AttachmentContract;
using BackendTask.Domain.Models.Attachment;

namespace BackendTask.API.Controllers.BankCards
{
    public class BankCardsController(ICurrentUserService currentUserService,
        IBankCardManager bkManager,
        IAttachmentManager attachmentManager,
        IMapper mapper, IStringLocalizerFactory factory)
        : BaseApiController(factory)
    {
        private readonly IBankCardManager _bkManager = bkManager;
        private readonly IAttachmentManager _attachmentManager = attachmentManager;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IMapper _mapper = mapper;

        [HttpGet("Get")]
        //[Authorize(Permissions.BankCard.View)]
        public async Task<ActionResult<BankCardDomain>> GetById(int id)
        {
            var data = await _bkManager.GetAsync(id);

            return Ok(data);
        }

        [HttpGet("GetAll")]
        //[Authorize(Permissions.BankCard.View)]
        public async Task<ActionResult<PagedResultDto<BankCardDomain>>> GetAll([FromQuery] PagedAndSortedRequestDto request)
        {
            var data = await _bkManager.GetAllAsync(request);

            return Ok(data);
        }


        [HttpPost("Create")]
        //[Authorize(Permissions.BankCard.Create)]
        public async Task<ActionResult<BankCardDomain>> Create([FromBody] CreateBankCardDomain input)
        {
            await _bkManager.AddAsync(input);

            return Ok();
        }


        [HttpPut("Update")]
        //[Authorize(Permissions.BankCard.Update)]
        public async Task<ActionResult<BankCardDomain>> Update([FromBody] BankCardDomain input)
        {
            await _bkManager.UpdateAsync(input);

            return Ok();
        }

        [HttpDelete("Delete")]
        //[Authorize(Permissions.BankCard.Delete)]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _bkManager.DeleteAsync(id);

            return Ok();
        }

        /// <summary>
        /// Create and save attachment.
        /// [Permissions.Attachment.Create]
        /// </summary>
        //[Authorize(Permissions.Attachment.Create)]
        [HttpPost("UploadFile")]
        public async Task<ActionResult<AttachmentDomain>> UploadFileAsync([FromForm] CreateAttachmentDto input)
        {
            var command = _mapper.Map<CreateAttachmentDomain>(input);

            var attachment = await _attachmentManager.CreateAndSaveAsync(command);

            return Ok(attachment);
        }
    }
}


