using System.ComponentModel.DataAnnotations;

namespace TakeHome.Models
{
    public class PersonModel
    {
        [Required(ErrorMessage = "A valid Id is required"), Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
