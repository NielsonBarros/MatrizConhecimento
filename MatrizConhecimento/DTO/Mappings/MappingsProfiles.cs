using AutoMapper;
using MatrizConhecimento.Models;

namespace MatrizConhecimento.DTO.Mappings
{
    public class MappingsProfiles : Profile
    {
        public MappingsProfiles()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Topic, TopicDTO>().ReverseMap();
            CreateMap<Matter, MatterDTO>().ReverseMap();
            CreateMap<Rating, RatingDTO>().ReverseMap();
            CreateMap<RatingHistory, RatingHistoryDTO>().ReverseMap();
        }
    }
}
