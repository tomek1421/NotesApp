using System.Linq.Expressions;
using NotesApp.Entities;

namespace NotesApp.RepositoryContracts;

/// <summary>
/// Represents data access logic for managing Subject entity
/// </summary>

public interface ISubjectsRepository
{
    Subject AddSubject(Subject subject);
    
    List<Subject> GetAllSubjects();
    
    /*Expression<Func<Subject, bool>>*/
    List<Subject> GetFilteredSubjects(Func<Subject, bool> predicate);
    
    Subject? GetSubjectById(Guid subjectId);
    
    Subject UpdateSubject(Subject subject);
    
    bool DeleteSubject(Guid subjectId);
    
    Subject? GetNotesBySubjectId(Guid subjectId);
}