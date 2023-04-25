using AutoMapper;
using Microsoft.Extensions.Logging;
using SMBTools.Contract.Filters;
using SMBTools.Web.BLL.Helpers;
using SMBTools.Web.BLL.Models;
using SMBTools.Web.BLL.Services.Interfaces;
using SMBTools.Web.DAL.DataModels;
using SMBTools.Web.DAL.Repositories.Interfaces;

namespace SMBTools.Web.BLL.Services;

public class AccountService : IAccountService
{
    private readonly ILogger<AccountService> _logger;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly AuthenticationHelper _authenticationHelper;

    public AccountService(ILogger<AccountService> logger,
        IAccountRepository accountRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        AuthenticationHelper authenticationHelper)
    {
        _logger = logger;
        _accountRepository = accountRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _authenticationHelper = authenticationHelper;
    }

    public Task CreateAsync(AccountModel accountModel)
    {
        var accountDataModel = _mapper.Map<AccountDataModel>(accountModel);
        accountDataModel.Password = _authenticationHelper.Hash(accountModel.Password);
        _accountRepository.Create(accountDataModel);

        return _unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await _accountRepository.DeleteAsync(id);
        await _unitOfWork.SaveAsync();
    }

    public async Task<AccountModel> GetByIdAsync(Guid id)
    {
        var accountDataModel = await _accountRepository.GetByIdAsync<AccountDataModel>(id);
        var accountModel = _mapper.Map<AccountModel>(accountDataModel);

        return accountModel;
    }

    public async Task<List<AccountModel>> GetByFilterAsync(AccountFilter filter)
    {
        var accountDataModels = await _accountRepository.GetByFilterAsync<AccountDataModel>(filter);
        var accountModel = _mapper.Map<List<AccountModel>>(accountDataModels);

        return accountModel;
    }
}