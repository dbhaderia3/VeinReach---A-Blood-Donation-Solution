using System.ComponentModel.DataAnnotations;
namespace Blood_Donation.Models
{
    public class RegisterDonor
    {
        [Key]
        public int RegId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(18, 120, ErrorMessage = "Age must be between 18 and 120.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Blood Group is required.")]
        public string BloodGroup { get; set; }

        public string AnyDisease { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
    }
}
