using AutoMapper;
using SMBTools.Web.DAL.DataModels;
using SMBTools.Web.DAL.Models;

namespace SMBTools.Web.DAL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDataModel>().ReverseMap();
        }
    }
}