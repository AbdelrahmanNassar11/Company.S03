using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.S03.PL.Dtos
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Range( 18, 60, ErrorMessage = "Age must be between 18 and 60!")]
        public int? Age { get; set; }
        [DataType(DataType.EmailAddress,ErrorMessage ="Email is not valid!")]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }

        [RegularExpression(@"^\d+\-[A-Za-z]+\-[A-Za-z]+(\-[A-Za-z]+)?$", ErrorMessage = "Address Must Be Like 123-Street-City or 123-Street-City-Country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }

        [DisplayName("CreateAt")]
        public DateTime CreateAt { get; set; }
        [DisplayName("Department")]
        public int? DepartmentId { get; set; }
        public string? ImageName { get; set; }
        public IFormFile? Image { get; set; }
    }
}
