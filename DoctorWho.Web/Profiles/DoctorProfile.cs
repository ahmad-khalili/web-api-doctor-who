using AutoMapper;
using DoctorWho.Db.Entities;
using DoctorWho.Db.Models;

namespace DoctorWho.Web.Profiles;

public class DoctorProfile : Profile
{
    public DoctorProfile()
    {
        CreateMap<Doctor, DoctorDto>();
        CreateMap<DoctorForUpsertionDto, Doctor>();
    }
}