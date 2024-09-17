using NotesApp.DTO;
using NotesApp.Entities;

namespace NotesApp.ServiceContracts;
public interface ISubjectsService
{
    SubjectResponse AddSubject(SubjectAddRequest? subjectAddRequest);

    List<SubjectResponse> GetAllSubjects();
    
    SubjectResponse? GetSubjectById(Guid? subjectId);
    
    SubjectResponse UpdateSubject(Guid? subjectId, SubjectUpdateRequest? subjectUpdateRequest);
    
    bool DeleteSubject(Guid? subjectId);
    
    SubjectWithNotesResponse? GetNotesBySubjectId(Guid? subjectId);
}