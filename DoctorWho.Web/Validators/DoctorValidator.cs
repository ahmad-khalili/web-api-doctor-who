using DoctorWho.Db.Entities;
using DoctorWho.Db.Models;
using FluentValidation;

namespace DoctorWho.Web.Validators;

public class DoctorValidator : AbstractValidator<Doctor>
{
    public DoctorValidator()
    {
        RuleFor(doctor => doctor.DoctorName).NotEmpty();
        RuleFor(doctor => doctor.DoctorNumber).NotEmpty();
        RuleFor(doctor => doctor.LastEpisodeDate).GreaterThanOrEqualTo(doctor => doctor.FirstEpisodeDate);
        RuleFor(doctor => doctor.LastEpisodeDate)
            .Null().When(doctor => doctor.FirstEpisodeDate == default)
            .WithMessage("LastEpisodeDate can't have a value when FirstEpisodeDate is empty");
    }
}