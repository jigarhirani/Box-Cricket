using System.ComponentModel.DataAnnotations;

namespace BOXCricket.Areas.SEC_User.Models
{
    public class SEC_UserModel
    {
        public int UserID { get; set; }

        //[Required]
        [Required(ErrorMessage = "Please Enter User Name")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
