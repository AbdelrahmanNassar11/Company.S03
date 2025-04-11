using System.ComponentModel.DataAnnotations;

namespace Company.S03.PL.Dtos
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Password Is Required!!")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%^&+=!]).{8,}$",
         ErrorMessage = "Password must be at least 8 characters long, contain upper & lower case letters, numbers, and special characters.")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Password Is Required!!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
