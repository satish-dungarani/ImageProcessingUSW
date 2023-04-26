
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ImageProcessing.Web.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name="First Name")]
        public string Firstname { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Profile Picture")]
        public string? Image { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime? DOB { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Gender")]
        public string? Gender { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = "Is Account Active?")]
        public bool? IsActive { get; set; }
        
        [Display(Name = "Are You Admin?")]
        public bool? IsAdmin { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
