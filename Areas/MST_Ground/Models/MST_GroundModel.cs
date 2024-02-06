using System.ComponentModel.DataAnnotations;

namespace BOXCricket.Areas.MST_Ground.Models
{
    public class MST_GroundModel
    {
        public int? GroundID { get; set; }

        [Required(ErrorMessage = "Please enter the Ground Name.")]
        public string GroundName { get; set; }

        [Required(ErrorMessage = "Please enter the Ground Nick Name.")]
        public string? GroundNickName { get; set; }

        [Required(ErrorMessage = "Please choose the BOX Cricket.")]
        public int BOXCricketID { get; set; }
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please enter the Ground Capacity.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid positive integer value for Ground Capacity.")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Please enter a valid positive integer value for Ground Capacity.")]
        public int GroundCapacity { get; set; }

        [Required(ErrorMessage = "Please enter the Ground Height.")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid positive value for Ground Height.")]
        public decimal GroundHeight { get; set; }

        [Required(ErrorMessage = "Please enter the Ground Width.")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid positive value for Ground Width.")]
        public decimal GroundWidth { get; set; }

        [Required(ErrorMessage = "Please enter the Ground Height.")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid positive value for Ground Height.")]
        public decimal GroundLength { get; set; }

        [Required(ErrorMessage = "Please enter the Name of Contact Person.")]
        public string ContactPersonName { get; set; }

        [Required(ErrorMessage = "Please enter the Number of Contact Person.")]
        public string ContactPersonNumber { get; set; }

        [Required(ErrorMessage = "Please indicate whether booking on this ground is allowed or not.")]
        public bool IsAllowedBooking { get; set; } = false;

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}