using NotesApp.DTO;
using NotesApp.Entities;
using NotesApp.Enums;

namespace NotesApp.ServiceContracts;
public interface ISubjectsService
{
    SubjectResponse AddSubject(SubjectAddRequest? subjectAddRequest);

    List<SubjectWithNotesCountResponse> GetAllSubjects();

    List<SubjectWithNotesCountResponse> GetFilteredSubjects(string searchBy, string searchString);

    List<SubjectWithNotesCountResponse> GetSortedSubjects(List<SubjectWithNotesCountResponse> subjects, string sortBy, SortOrderOptions sortOrder);
    
    SubjectResponse? GetSubjectById(Guid? subjectId);
    
    SubjectResponse UpdateSubject(Guid? subjectId, SubjectUpdateRequest? subjectUpdateRequest);
    
    bool DeleteSubject(Guid? subjectId);
    
    SubjectWithNotesResponse? GetNotesBySubjectId(Guid? subjectId);
}