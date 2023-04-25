using AutoMapper;
using SMBTools.Contract.Models.Requests;
using SMBTools.Web.BLL.Models;

namespace SMBTools.Web.Api.Mapper
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<AccountRequestDto, AccountModel>().ReverseMap();
        }
    }
}