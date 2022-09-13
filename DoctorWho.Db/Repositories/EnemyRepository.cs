using DoctorWho.Db.Entities;

namespace DoctorWho.Db.Repositories;

public class EnemyRepository
{
    private readonly DoctorWhoCoreDbContext _context;

    public EnemyRepository(DoctorWhoCoreDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Enemy GetEnemyById(int enemyId)
    {
        var enemy = _context.Enemies.FirstOrDefault(e => e.EnemyId == enemyId);
        if (enemy == default)
            throw new Exception($"Enemy with Id({enemyId}) was not found!");
        return enemy;
    }
    
    public void AddEnemy(Enemy enemy)
    {
        _context.Enemies.Add(enemy);
        _context.SaveChanges();
    }

    public void ModifyEnemyName(Enemy enemy, string newName)
    {
        enemy.EnemyName = newName;
        _context.SaveChanges();
    }
    
    public void ModifyEnemyDescription(Enemy enemy, string newDescription)
    {
        enemy.Description = newDescription;
        _context.SaveChanges();
    }

    public void DeleteEnemy(int enemyId)
    {
        var enemyToDelete = _context.Enemies.FirstOrDefault(e => e.EnemyId.Equals(enemyId));

        if (enemyToDelete == default)
            throw new Exception($"Enemy with id {enemyId} not found!");
        
        _context.Enemies.Remove(enemyToDelete);
        _context.SaveChanges();
    }
    
    public string? GetEnemiesForEpisode(int episodeId)
    {
        var companions = _context.Episodes.Select(e => _context.GetEnemies(episodeId)).FirstOrDefault();
        return companions;
    }
}