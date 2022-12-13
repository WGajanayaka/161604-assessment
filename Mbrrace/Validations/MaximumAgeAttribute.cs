using System.ComponentModel.DataAnnotations;

namespace Mbrrace.Validations
{
    public class MaximumAgeAttribute : ValidationAttribute
    {
        public MaximumAgeAttribute(int maximumAge)
        {
            MaximumAge = maximumAge;
            ErrorMessage = "{0} must be someone bellow {1} years of age";
        }

        public override bool IsValid(object value)
        {
            DateTime date;
            if ((value != null && DateTime.TryParse(value.ToString(), out date)))
            {
                return date.AddYears(MaximumAge) > DateTime.Now;
            }
            if (value == null)
            {
                return true;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, MaximumAge);
        }

        public int MaximumAge { get; }
    }
}
