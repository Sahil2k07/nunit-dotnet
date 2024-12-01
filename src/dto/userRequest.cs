using System.ComponentModel.DataAnnotations;

namespace nunit.dto
{
    public class SignupRequest
    {
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "Email Required")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "FirstName Required")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "LastName Required")]
        public required string LastName { get; set; }
    }

    public class UserDetailsRequest
    {
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "Email required")]
        public required string Email { get; set; }
    }

    public class UpdateUserDetailsRequest
    {
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "Email Required")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "FirstName Required")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "LastName Required")]
        public required string LastName { get; set; }
    }

    public class DeleteAccountRequest
    {
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "Email required")]
        public required string Email { get; set; }
    }
}
