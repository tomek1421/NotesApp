using System.Text.Json;
using NotesApp.DTO;
using NotesApp.Entities;
using NotesApp.Enums;
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
        
        //get date of creation
        subject.DateOfCreation = DateTime.Now.ToString("dd/MM/yyyy");
        
        _subjectsRepository.AddSubject(subject);
        
        return subject.ToSubjectResponse();
        
    }

    public List<SubjectWithNotesCountResponse> GetAllSubjects()
    {
        List<Subject> subjects = _subjectsRepository.GetAllSubjects();
        
        return subjects.Select(temp => temp.ToSubjectWithNotesCountResponse(temp?.Notes?.Count ?? 0)).ToList();
    }

    public List<SubjectWithNotesCountResponse> GetFilteredSubjects(string searchBy, string searchString)
    {
        List<Subject> filteredSubjects = searchBy switch
        {
            nameof(Subject.SubjectName) => _subjectsRepository.GetFilteredSubjects(temp =>
                temp.SubjectName != null && temp.SubjectName.ToLower().Contains(searchString.ToLower())),

            nameof(Subject.Hashtags) => _subjectsRepository.GetFilteredSubjects(temp =>
            {
                if (temp.Hashtags == null)
                    return false;
                
                List<string>? hashtagsList = JsonSerializer.Deserialize<List<string>>(temp.Hashtags);

                if (hashtagsList == null)
                    return false;
                
                return hashtagsList.Contains(searchString);
            }),
            
            _ => _subjectsRepository.GetAllSubjects()
            
        };

        return filteredSubjects.Select(temp => temp.ToSubjectWithNotesCountResponse(temp?.Notes?.Count ?? 0)).ToList();
    }

    public List<SubjectWithNotesCountResponse> GetSortedSubjects(List<SubjectWithNotesCountResponse> subjects, string sortBy, SortOrderOptions sortOrder)
    {
        return (sortBy, sortOrder) switch
        {
            (nameof(SubjectWithNotesCountResponse.SubjectName), SortOrderOptions.ASC) => subjects.OrderBy(temp => temp.SubjectName).ToList(),
            (nameof(SubjectWithNotesCountResponse.SubjectName), SortOrderOptions.DESC) => subjects.OrderByDescending(temp => temp.SubjectName).ToList(),
            (nameof(SubjectWithNotesCountResponse.NotesCount), SortOrderOptions.ASC) => subjects.OrderBy(temp => temp.NotesCount).ToList(),
            (nameof(SubjectWithNotesCountResponse.NotesCount), SortOrderOptions.DESC) => subjects.OrderByDescending(temp => temp.NotesCount).ToList(),
            (nameof(SubjectWithNotesCountResponse.DateOfCreation), SortOrderOptions.ASC) => subjects.OrderBy(temp =>
            {
                DateTime dateTime;
                
                if (string.IsNullOrEmpty(temp.DateOfCreation))
                {
                    return DateTime.MinValue;
                }

                try
                {
                    dateTime = DateTime.Parse(temp.DateOfCreation);
                }
                catch (Exception)
                {
                    dateTime = DateTime.MinValue;
                }
                
                return dateTime;

            }).ToList(),
            (nameof(SubjectWithNotesCountResponse.DateOfCreation), SortOrderOptions.DESC) => subjects.OrderByDescending(temp =>
            {
                DateTime dateTime;
                
                if (string.IsNullOrEmpty(temp.DateOfCreation))
                {
                    return DateTime.MinValue;
                }

                try
                {
                    dateTime = DateTime.Parse(temp.DateOfCreation);
                }
                catch (Exception)
                {
                    dateTime = DateTime.MinValue;
                }
                
                return dateTime;

            }).ToList(),
        };
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

    public SubjectWithNotesResponse? GetNotesBySubjectId(Guid? subjectId)
    {
        if (subjectId == null)
            throw new ArgumentNullException((nameof(subjectId)));
        
        Subject? subject = _subjectsRepository.GetNotesBySubjectId(subjectId.Value);
        
        if (subject == null)
            return null;
        
        List<NoteResponse>? subjectNoteResponseList = subject.Notes?.Select(temp => temp.ToNoteResponse()).ToList();

        if (subjectNoteResponseList == null)
            subjectNoteResponseList = new List<NoteResponse>();
        
        return subject.ToSubjectWithNotesResponse(subjectNoteResponseList);
    }
}