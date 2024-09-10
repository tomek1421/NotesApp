using NotesApp.DTO;

namespace NotesApp.ServiceContracts;
public interface ISubjectsService
{
    SubjectResponse AddSubject(SubjectAddRequest? subjectAddRequest);

    List<SubjectResponse> GetAllSubjects();
    
    SubjectResponse UpdateSubject(SubjectUpdateRequest? subjectUpdateRequest);
    
    bool DeleteSubject(Guid? subjectId);
}