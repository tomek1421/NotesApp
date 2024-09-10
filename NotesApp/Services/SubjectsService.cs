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

    private SubjectResponse ConvertSubjectToSubjectResponse(Subject subject)
    {
        SubjectResponse subjectResponse = new SubjectResponse()
        {
            SubjectId = subject.SubjectId,
            SubjectName = subject.SubjectName,
            SubjectDescription = subject.SubjectDescription
        };
        
        return subjectResponse;
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
        
        return ConvertSubjectToSubjectResponse(subject);
        
    }

    public List<SubjectResponse> GetAllSubjects()
    {
        List<Subject> subject = _subjectsRepository.GetAllSubjects();
        
        return subject.Select(temp => ConvertSubjectToSubjectResponse(temp)).ToList();
    }

    public SubjectResponse UpdateSubject(SubjectUpdateRequest? subjectUpdateRequest)
    {
        if (subjectUpdateRequest == null)
            throw new ArgumentNullException((nameof(subjectUpdateRequest)));
        
        //validation
        ValidationHelper.ModelValidation(subjectUpdateRequest);
        
        //get matching subject
        Subject? matchingSubject = _subjectsRepository.GetSubjectById(subjectUpdateRequest.SubjectId);

        //Chnage to messange instead exception
        
        if (matchingSubject == null)
            throw new ArgumentException("Given subject id does not exist");
        
        matchingSubject.SubjectName = subjectUpdateRequest.SubjectName;
        matchingSubject.SubjectDescription = subjectUpdateRequest.SubjectDescription;
        
        _subjectsRepository.UpdateSubject(matchingSubject);

        return ConvertSubjectToSubjectResponse(matchingSubject);
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
}