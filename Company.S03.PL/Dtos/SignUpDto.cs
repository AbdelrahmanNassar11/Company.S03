using System.ComponentModel.DataAnnotations;

namespace Company.S03.PL.Dtos
{
    public class SignUpDto
    {
        [Required(ErrorMessage = "UserName Is Required!!")] 
        public string UserName { get; set; }
        [Required(ErrorMessage = "FirstName Is Required!!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName Is Required!!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Password Is Required!!")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%^&+=!]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long, contain upper & lower case letters, numbers, and special characters.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email Is Required!!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "ConfirmPassword  Is Required!!")]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirm Password do not match!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
