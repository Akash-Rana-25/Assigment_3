using System.ComponentModel.DataAnnotations;

namespace Assingment_3.DTO
{
    public class CustomerViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required]
        [Display(Name = "Contact No.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact No. should be 10 digits.")]
        public string? ContactNo { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        //[RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@#$%^&+=]).{8,12}$", ErrorMessage = "Password must meet the specified criteria.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }
    }
}
