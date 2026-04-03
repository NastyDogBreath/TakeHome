using System.ComponentModel.DataAnnotations;

namespace TakeHome.Models
{
    public class PatientModel : PersonModel
    {
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public GuardianModel? Guardian { get; set; }
    }
}
