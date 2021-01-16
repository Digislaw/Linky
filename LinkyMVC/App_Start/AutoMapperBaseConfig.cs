﻿using AutoMapper;
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

            CreateMap<Link, LinkUpdateModel>()
                .ForMember(x => x.Id, d => d.MapFrom(s => s.Id))
                .ForMember(x => x.Label, d => d.MapFrom(s => s.Label))
                .ForMember(x => x.URL, d => d.MapFrom(s => s.URL))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<LinkUpdateModel, Link>()
                .ForMember(x => x.Id, d => d.MapFrom(s => s.Id))
                .ForMember(x => x.Label, d => d.MapFrom(s => s.Label))
                .ForMember(x => x.URL, d => d.MapFrom(s => s.URL))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}