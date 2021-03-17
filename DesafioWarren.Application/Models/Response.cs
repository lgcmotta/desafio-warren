using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace DesafioWarren.Application.Models
{
    public class Response
    {
        public object Payload { get; set; }
        
        public List<ValidationFailure> Failures { get; } = new();
        
        public Response(object payload)
        {
            Payload = payload;
        }

        public Response()
        {
                
        }

        public void AddValidationFailure(ValidationFailure validationFailure) => Failures.Add(validationFailure);
        
        public void AddValidationFailures(IEnumerable<ValidationFailure> validationFailures) => Failures.AddRange(validationFailures);

        public void RemoveValidationFailure(ValidationFailure validationFailure) => Failures.Remove(validationFailure);

        public void ClearValidationErrors() => Failures.Clear();
        
        public bool IsErrorResponse() => Failures.Any();
    }
  
}