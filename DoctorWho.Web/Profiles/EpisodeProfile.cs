using AutoMapper;
using DoctorWho.Db.Entities;
using DoctorWho.Db.Models;

namespace DoctorWho.Web.Profiles;

public class EpisodeProfile : Profile
{
    public EpisodeProfile()
    {
        CreateMap<Episode, EpisodeDto>();
    }
}