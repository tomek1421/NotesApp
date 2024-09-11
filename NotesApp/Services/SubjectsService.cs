using NotesApp.DTO;
using NotesApp.Entities;
using NotesApp.Helpers;
using NotesApp.RepositoryContracts;
using NotesApp.ServiceContracts;

namespace NotesApp.Services;

public class SubjectsService : ISubjectsService
{
    private readonly ISubjectsRepository _subjectsRepository;
    private readonly ILogger<SubjectsService> _logger;

    //constructor
    public SubjectsService(ISubjectsRepository subjectsRepository, ILogger<SubjectsService> logger)
    {
        _subjectsRepository = subjectsRepository;
        _logger = logger;
    }
    
    public SubjectResponse AddSubject(SubjectAddRequest? subjectAddRequest)
    {
        if (subjectAddRequest == null)
            throw new ArgumentNullException((nameof(subjectAddRequest)));

        //model validation
        ValidationHelper.ModelValidation(subjectAddRequest);
        
        //convert SubjectAddRequest to Subject
        Subject subject = subjectAddRequest.ToSubject();
        
        //generate SubjectId
        subject.SubjectId = Guid.NewGuid();
        
        _subjectsRepository.AddSubject(subject);
        
        return subject.ToSubjectResponse();
        
    }

    public List<SubjectResponse> GetAllSubjects()
    {
        List<Subject> subject = _subjectsRepository.GetAllSubjects();
        
        return subject.Select(temp => temp.ToSubjectResponse()).ToList();
    }

    public SubjectResponse? GetSubjectById(Guid? subjectId)
    {
        if (subjectId == null)
            return null;
        
        Subject? subject = _subjectsRepository.GetSubjectById(subjectId.Value);

        if (subject == null)
            return null;
        
        return subject.ToSubjectResponse();
    }

    public SubjectResponse UpdateSubject(Guid? subjectId, SubjectUpdateRequest? subjectUpdateRequest)
    {
        if (subjectId == null || subjectUpdateRequest == null)
            throw new ArgumentNullException();
        
        //validation
        ValidationHelper.ModelValidation(subjectUpdateRequest);
        
        //get matching subject
        Subject? matchingSubject = _subjectsRepository.GetSubjectById(subjectId.Value);

        //Chanage to messange instead exception
        
        if (matchingSubject == null)
            throw new ArgumentException("Given subject id does not exist");
        
        matchingSubject.SubjectName = subjectUpdateRequest.SubjectName;
        matchingSubject.SubjectDescription = subjectUpdateRequest.SubjectDescription;
        
        _subjectsRepository.UpdateSubject(matchingSubject);

        return matchingSubject.ToSubjectResponse();
    }

    public bool DeleteSubject(Guid? subjectId)
    {
        if (subjectId == null)
            throw new ArgumentNullException((nameof(subjectId)));
        
        Subject? subject = _subjectsRepository.GetSubjectById(subjectId.Value);
        
        if (subject == null)
            return false;
        
        _subjectsRepository.DeleteSubject(subjectId.Value);

        return true;
    }

    public List<NoteResponse>? GetNotesBySubjectId(Guid? subjectId)
    {
        if (subjectId == null)
            throw new ArgumentNullException((nameof(subjectId)));
        
        List<Note>? subjectNotesList = _subjectsRepository.GetNotesBySubjectId(subjectId.Value);
        
        List<NoteResponse>? subjectNoteResponseList = subjectNotesList?.Select(temp => temp.ToNoteResponse()).ToList();

        return subjectNoteResponseList;
    }
}