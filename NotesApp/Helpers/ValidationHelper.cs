using System.ComponentModel.DataAnnotations;

namespace NotesApp.Helpers;

public class ValidationHelper
{
    //Model validation
    internal static void ModelValidation(object obj)
    {
        ValidationContext validationContext = new ValidationContext(obj);
        List<ValidationResult> validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
        if (!isValid)
        {
            throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
        }
    }
}