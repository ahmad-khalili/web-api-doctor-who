using DoctorWho.Db.Entities;

namespace DoctorWho.Db.Repositories;

public class AuthorRepository
{
    private readonly DoctorWhoCoreDbContext _context;

    public AuthorRepository(DoctorWhoCoreDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public void AddAuthor(Author author)
    {
        _context.Authors.Add(author);
        _context.SaveChanges();
    }

    public void ModifyAuthor(Author author, string newName)
    {
        author.AuthorName = newName;
        _context.SaveChanges();
    }

    public void DeleteAuthor(int authorId)
    {
        var authorToDelete = _context.Authors.FirstOrDefault(a => a.AuthorId.Equals(authorId));

        if (authorToDelete == default)
            throw new Exception($"Author with id {authorId} not found!");
        
        _context.Authors.Remove(authorToDelete);
        _context.SaveChanges();
    }
}