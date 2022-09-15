using AutoMapper;
using DoctorWho.Db.Entities;
using DoctorWho.Db.Models;

namespace DoctorWho.Web.Profiles;

public class CompanionProfile : Profile
{
    public CompanionProfile()
    {
        CreateMap<Companion, CompanionDto>();
        CreateMap<CompanionForCreationDto, Companion>();
    }
}