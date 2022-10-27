using AngularTestBackendServer.Core.Enums;
using AngularTestBackendServer.Core.Models;
using AutoMapper;

namespace AngularTestBackendServer.Data.Entities.Mappings.Profiles;

public class NflProfile : Profile
{
    public NflProfile()
    {
        CreateMap<SeasonEntity, Season>();
        
        CreateMap<TeamEntity, Team>()
            .ForMember(dest => dest.Division,
                opt => opt.MapFrom(x => x.Division.Name))
            .ForMember(dest => dest.Conference,
                opt => opt.MapFrom(x => x.Conference.Name));
        
        CreateMap<DivisionEntity, Division>().ConstructUsing(src => (Division) src.Id);
    }
}