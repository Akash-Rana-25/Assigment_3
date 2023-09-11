using System.ComponentModel.DataAnnotations;

namespace Assingment_3.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact No. should be 10 digits.")]
        public string? ContactNo { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        //[RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@#$%^&+=]).{8,12}$", ErrorMessage = "Password must meet the specified criteria.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

      

        public DateTime DateAdded { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
