using AutoMapper;
using DoctorWho.Db.Entities;
using DoctorWho.Db.Models;

namespace DoctorWho.Web.Profiles;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<AuthorForUpdateDto, Author>();
    }
}