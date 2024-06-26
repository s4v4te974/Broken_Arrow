﻿using AutoMapper;
using MilitaryNuclearAccident.Src.Mna.Common.DbSet;
using MilitaryNuclearAccident.Src.Mna.Common.Dtos;

namespace MilitaryNuclearAccident.Src.Mna.UI.Controllers.Profiles
{
    public class DescriptionProfile : Profile
    {
        public DescriptionProfile()
        {
            CreateMap<Description, DescriptionResponse>()
                 .ForMember(dest => dest.BrokenArrowId,
                 src => src.MapFrom(b => b.BrokenArrow != null ? b.BrokenArrow.BrokenArrowId : (Guid?)null));

        }
    }
}