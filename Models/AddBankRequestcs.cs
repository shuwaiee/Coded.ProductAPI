using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models
{
    public class AddBankRequest : IValidatableObject
    {
        [Required]
        public string Name { get; set; }
        [Url]
        public string Location { get; set; }
        [MinLength(10)]
        public string BranchManager { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
           if (Name.StartsWith("N"))
            {
               yield return new ValidationResult("Name can not start wih N");
            }
        }
    }
    public class BankBranchResponse
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string BranchManager { get; set; }
    }

}
