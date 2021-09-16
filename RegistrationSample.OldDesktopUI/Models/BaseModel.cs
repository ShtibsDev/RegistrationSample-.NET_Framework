using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RegistrationSample.OldDesktopUI.Models
{
    public class BaseModel : ObservableObject, IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                var validationResults = new List<ValidationResult>();

                return Validator.TryValidateProperty(
                        GetType().GetProperty(columnName).GetValue(this), new ValidationContext(this) { MemberName = columnName }, validationResults)
                    ? null
                    : validationResults.First().ErrorMessage;
            }
        }

        public string Error { get; }
    }
}
