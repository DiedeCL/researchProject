using System.ComponentModel.DataAnnotations;

namespace researchproject.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        public int CompanyId { get; set; }

    }
}