using AutoMapper;
using Linky.Entities.Models;
using LinkyMVC.Models.InputModels;

namespace LinkyMVC.App_Start
{
    public class AutoMapperBaseConfig : Profile
    {
        public AutoMapperBaseConfig()
        {
            
            CreateMap<LinkInputModel, Link>()
                .ForMember(x => x.Id, d => d.Ignore())
                .ForMember(x => x.Clicks, d => d.Ignore())
                .ForMember(x => x.CreatedAt, d => d.Ignore())
                .ForMember(x => x.ApplicationUser, d => d.Ignore())
                .ForMember(x => x.ApplicationUserId, d => d.Ignore());

        }
    }
}