using DoctorWho.Db.Entities;

namespace DoctorWho.Db.Repositories;

public class CompanionRepository
{
    private readonly DoctorWhoCoreDbContext _context;

    public CompanionRepository(DoctorWhoCoreDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public Companion GetCompanionById(int companionId)
    {
        var companion = _context.Companions.FirstOrDefault(c => c.CompanionId == companionId);
        if (companion == default)
            throw new Exception($"Companion with Id({companionId}) was not found!");
        return companion;
    }
    
    public void AddCompanion(Companion companion)
    {
        _context.Companions.Add(companion);
        _context.SaveChanges();
    }

    public void ModifyCompanionName(Companion companion, string newName)
    {
        companion.CompanionName = newName;
        _context.SaveChanges();
    }
    
    public void ModifyCompanionActor(Companion companion, string newActorName)
    {
        companion.WhoPlayed = newActorName;
        _context.SaveChanges();
    }

    public void DeleteCompanion(int companionId)
    {
        var companionToDelete = _context.Companions.FirstOrDefault(c => c.CompanionId.Equals(companionId));

        if (companionToDelete == default)
            throw new Exception($"Companion with id {companionId} not found!");
        
        _context.Companions.Remove(companionToDelete);
        _context.SaveChanges();
    }
    
    public string? PrintCompanionsForEpisode(int episodeId)
    {
        var companions = _context.Episodes.Select(e => _context.GetCompanions(episodeId)).FirstOrDefault();
        return companions;
    }
}