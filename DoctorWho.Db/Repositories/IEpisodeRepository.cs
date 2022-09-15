using DoctorWho.Db.Entities;
using DoctorWho.Db.Services;

namespace DoctorWho.Db.Repositories;

public interface IEpisodeRepository
{
    Task<(IEnumerable<Episode>, PaginationMetadata)> GetEpisodesAsync(int pageNumber, int pageSize);
}