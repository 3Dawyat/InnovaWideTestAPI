using AutoMapper;
using InnovaWideTest.Domain.DTOs;
using InnovaWideTest.Domain.Entities;

namespace InnovaWideTest.API.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Case, CaseDto>()
                 .ReverseMap()
                 .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<Lawyer, LawyerDto>()
                      .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Hearing, HearingDto>()
                 .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Hearing, HearingListDto>()
                   .ForMember(dest => dest.Case, opt => opt.MapFrom(src => src.Case.Name))
                   .ForMember(dest => dest.Lawyer, opt => opt.MapFrom(src => src.Lawyer.Name))
                 .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
