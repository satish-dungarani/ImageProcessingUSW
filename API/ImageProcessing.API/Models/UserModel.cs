
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ImageProcessing.API.Models
{
    /// <summary>
    /// USer Model for map
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Primery key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// First Name
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [Display(Name="First Name")]
        public string Firstname { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string? Lastname { get; set; }
        /// <summary>
        /// Email
        /// </summary>
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
        [JsonIgnore]
        [DataType(DataType.Text)]
        [Display(Name = "Is Account Active?")]
        public bool? IsActive { get; set; }
        [JsonIgnore]
        [Display(Name = "Are You Admin?")]
        public bool? IsAdmin { get; set; }
        [JsonIgnore]
        public DateTime? CreatedOn { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedOn { get; set; }
        [JsonIgnore]
        [Display(Name = "Profile Picture")]
        public IFormFile? ProfilePicture { get; set; }
        [JsonIgnore]
        [Display(Name = "Profile Picture URL")]
        public string? ProfilePictureUrl { get; set; }
    }
}
