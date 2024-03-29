using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BOXCricket.Areas.MST_User.Models
{
    public class MST_UserModel
    {
        public int? UserID { get; set; }

        [Required(ErrorMessage = "Please Enter First Name")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "Please Enter Last Name")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please Enter the Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Password should contain at least one lowercase letter, one uppercase letter, one digit, and one special character. Minimum length is 8 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Your Phone No.")]
        public string Contact { get; set; }

        public int? CountryID { get; set; }

        public int? StateID { get; set; }

        public int? CityID { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }

        public bool? IsEmailConfirmed { get; set; }

        public string? VarificationToken { get; set; }

        public IFormFile? File { get; set; }

        public string? ProfilePhotoPath { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class MST_User_PasswrodChange
    {
        [Required(ErrorMessage = "Please Enter the Current Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Password should contain at least one lowercase letter, one uppercase letter, one digit, and one special character. Minimum length is 8 characters.")]
        public string CurruntPassword { get; set; }

        [Required(ErrorMessage = "Please Enter the New Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Password should contain at least one lowercase letter, one uppercase letter, one digit, and one special character. Minimum length is 8 characters.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please Confirm the Password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class MST_User_PasswrodReset
    {
        [Required(ErrorMessage = "Please Enter the New Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Password should contain at least one lowercase letter, one uppercase letter, one digit, and one special character. Minimum length is 8 characters.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please Confirm the Password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string? Email { get; set; }
    }
}
