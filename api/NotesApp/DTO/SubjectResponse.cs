using NotesApp.Entities;

namespace NotesApp.DTO;

public class SubjectResponse
{
    public Guid SubjectId { get; set; }
    
    public string? SubjectName { get; set; }
    
    public string? SubjectDescription { get; set; }
    
    public string? DateOfCreation { get; set; }
    
    public string? Hashtags { get; set; }
}

public static class SubjectExtensions
{
    public static SubjectResponse ToSubjectResponse(this Subject subject)
    {
        SubjectResponse subjectResponse = new SubjectResponse()
        {
            SubjectId = subject.SubjectId,
            SubjectName = subject.SubjectName,
            SubjectDescription = subject.SubjectDescription,
            DateOfCreation = subject.DateOfCreation,
            Hashtags = subject.Hashtags
        };
        
        return subjectResponse;
    }
}