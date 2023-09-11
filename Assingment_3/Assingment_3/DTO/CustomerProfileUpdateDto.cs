using System.ComponentModel.DataAnnotations;

namespace Assingment_3.DTO
{
    public class CustomerProfileUpdateDto
    {
        [Required]
        [StringLength(255)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string? LastName { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Contact No. should be only numeric")]
        [StringLength(255)]
        public string? ContactNo { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match")]
        public string? ConfirmNewPassword { get; set; }

        public DateTime? AddedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
