using NotesApp.DTO;
using NotesApp.Entities;
using NotesApp.RepositoryContracts;

namespace NotesApp.Repositories;

public class SubjectsRepository : ISubjectsRepository
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<SubjectsRepository> _logger;
    
    public SubjectsRepository(ApplicationDbContext db, ILogger<SubjectsRepository> logger)
    {
        _db = db;
        _logger = logger;
    }
    
    public Subject AddSubject(Subject subject)
    {
        _db.Subjects.Add(subject);
        
        _db.SaveChanges();
        return subject;
    }

    public List<Subject> GetAllSubjects()
    {
        return _db.Subjects.ToList();
    }

    public Subject? GetSubjectById(Guid subjectId)
    {
        return _db.Subjects.FirstOrDefault(temp => temp.SubjectId == subjectId);
    }

    public Subject UpdateSubject(Subject subject)
    {
        Subject? matchingSubject = _db.Subjects.FirstOrDefault(temp => temp.SubjectId == subject.SubjectId);

        if (matchingSubject == null)
            return subject;
        
        matchingSubject.SubjectName = subject.SubjectName;
        matchingSubject.SubjectDescription = subject.SubjectDescription;
        
        _db.SaveChanges();
        return matchingSubject;
    }

    public bool DeleteSubject(Guid subjectId)
    {
        _db.Subjects.RemoveRange(_db.Subjects.Where(temp => temp.SubjectId == subjectId));
        
        int rowsDeleted = _db.SaveChanges();
        return rowsDeleted > 0;
    }
}