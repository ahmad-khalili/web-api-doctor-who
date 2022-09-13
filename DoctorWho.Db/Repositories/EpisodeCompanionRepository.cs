using DoctorWho.Db.Models;

namespace DoctorWho.Db.Repositories;

public class EpisodeCompanionRepository
{
    private readonly DoctorWhoCoreDbContext _context;

    public EpisodeCompanionRepository()
    {
        _context = new DoctorWhoCoreDbContext();
    }

    public void AddCompanionToEpisode(Companion companion, Episode episode)
    {
        if (!_context.Companions.Contains(companion)) throw new ArgumentException("Companion doesn't exist!");
        if (!_context.Episodes.Contains(episode)) throw new ArgumentException("Episode doesn't exist!");

        var newEpisodeCompanion = new EpisodeCompanion
        {
            CompanionId = companion.CompanionId,
            EpisodeId = episode.EpisodeId
        };

        _context.EpisodeCompanions.Add(newEpisodeCompanion);
    }
}