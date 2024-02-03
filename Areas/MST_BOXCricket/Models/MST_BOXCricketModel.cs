using System.ComponentModel.DataAnnotations;

namespace BOXCricket.Areas.MST_BOXCricket.Models
{
    public class MST_BOXCricketModel
    {
        public int? BOXCricketID { get; set; }

        [Required(ErrorMessage = "Please enter the BOX Cricket Name.")]
        public string BOXCricketName { get; set; }

        public int OwnerID { get; set; }

        [Required(ErrorMessage = "Please enter the Address of the BOX Cricket.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please choose the Country.")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Please choose the State.")]
        public int StateID { get; set; }

        [Required(ErrorMessage = "Please choose the City.")]
        public int CityID { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
