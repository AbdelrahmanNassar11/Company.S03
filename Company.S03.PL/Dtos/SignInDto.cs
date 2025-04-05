using System.ComponentModel.DataAnnotations;

namespace Company.S03.PL.Dtos
{
    public class SignInDto
    {
        [Required(ErrorMessage = "Password Is Required!!")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%^&+=!]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long, contain upper & lower case letters, numbers, and special characters.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email Is Required!!")]
        [EmailAddress]
        public string Email { get; set; }

        public bool RememberMe { get; set; }
    }
}
