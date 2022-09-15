using DoctorWho.Db.Entities;
using FluentValidation;

namespace DoctorWho.Web.Validators;

public class EpisodeValidator : AbstractValidator<Episode>
{
    public EpisodeValidator()
    {
        RuleFor(ep => ep.AuthorId).NotEmpty();
        RuleFor(ep => ep.DoctorId).NotEmpty();
        RuleFor(ep => ep.Notes).Custom((notes, context) =>
        {
            if (notes.Length != 10)
            {
                context.AddFailure("Notes should be 10 characters long");
            }
        });
        RuleFor(ep => ep.EpisodeNumber).GreaterThan(0);
    }
}