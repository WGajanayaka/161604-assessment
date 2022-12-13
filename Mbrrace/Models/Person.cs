using Mbrrace.Validations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mbrrace.Models
{
    [Table("People")]
    public class Person: AuditedEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Given Name")]
        public string GivenName { get; set; }

        [Required]
        [DisplayName("Family Name")]
        public string FamilyName { get; set; }

        [Required]
        [DisplayName("First line of Address")]
        public string Address1 { get; set; }

        [DisplayName("Second line of Address")]
        public string? Address2 { get; set; }

        [Required]
        public string Town { get; set; }

        [Required]
        [RegularExpression(@"^(([gG][iI][rR] {0,}0[aA]{2})|((([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y]?[0-9][0-9]?)|(([a-pr-uwyzA-PR-UWYZ][0-9][a-hjkstuwA-HJKSTUW])|([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y][0-9][abehmnprv-yABEHMNPRV-Y]))) {0,}[0-9][abd-hjlnp-uw-zABD-HJLNP-UW-Z]{2}))$", ErrorMessage = "{0} format is invalid")]
        //reference: https://en.wikipedia.org/wiki/Postcodes_in_the_United_Kingdom 
        public string Postcode { get; set; }

        [Required]
        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Date of Birth")]
        [MinimumAge(18, ErrorMessage = "{0} must be someone at least {1} years of age")]
        [MaximumAge(75, ErrorMessage = "{0} must be someone below {1} years of age")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }


        [NotMapped]
        [DisplayName("Name")]
        public string? FullName 
        {
            get 
            {
                return $"{GivenName} {FamilyName}";
            }
        }


        [NotMapped]
        public string? Address
        {
            get
            {
                return $"{Address1}, {Address2}";
            }
        }

        [NotMapped]
        public int? Age
        {
            get
            {
                if (DateOfBirth.HasValue && DateOfBirth.Value < DateTime.Now)
                {
                    return new DateTime((DateTime.Now - DateOfBirth.Value).Ticks).Year;
                }
                return null;
            }
        }

    }
}
