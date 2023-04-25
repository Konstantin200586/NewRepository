using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMBTools.Contract.Constants;
using SMBTools.Contract.Enums;
using SMBTools.Contract.Filters;
using SMBTools.Contract.Models.Requests;
using SMBTools.Web.BLL.Models;
using SMBTools.Web.BLL.Services.Interfaces;

namespace SMBTools.Web.Api.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAuthService authService,
            IAccountService accountService,
            ILogger<BaseController> logger,
            IMapper mapper) : base(logger)
        {
            _authService = authService;
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost(ApiControllers.Account.Login)]
        [AllowAnonymous]
        public async Task<IActionResult> AuthorizeAsync(LoginRequestDto loginRequestDto)
        {
            var loginModel = _mapper.Map<LoginModel>(loginRequestDto);
            return await ProcessRequest(() => _authService.GetUserTokenAsync(loginModel));
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> PostAsync(AccountRequestDto accountRequestDto)
        {
            var accountModel = _mapper.Map<AccountModel>(accountRequestDto);
            return await ProcessRequest<object>(() => _accountService.CreateAsync(accountModel));
        }

        [HttpGet("idUser")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var accountModel = await _accountService.GetByIdAsync(id);
            return Ok(accountModel);
        }

        [HttpPost("allUsers")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> GetByFilterAsync(AccountFilter filter)
        {
            var accountModels = await _accountService.GetByFilterAsync(filter);
            return Ok(accountModels);
        }

        [HttpDelete]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return await ProcessRequest<object>(() => _accountService.DeleteAsync(id));
        }
    }
}