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

        [Required(ErrorMessage = "Please Enter Hourly Rate in INR.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Please enter a valid amount in INR.")]
        public decimal ActualHourlyRate { get; set; }

        [Required(ErrorMessage = "Please Enter Hourly Rate in INR.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Please enter a valid amount in INR.")]
        public decimal DiscountedHourlyRate { get; set; }

        [Required(ErrorMessage = "Please indicate whether booking on this ground is allowed or not.")]
        public bool IsAllowedBooking { get; set; } = false;

        public string? GroundImagePath1 { get; set; }
        public string? GroundImagePath2 { get; set; }
        public string? GroundImagePath3 { get; set; }
        public string? GroundImagePath4 { get; set; }

        public IFormFile? GroundImage1 { get; set; }
        public IFormFile? GroundImage2 { get; set; }
        public IFormFile? GroundImage3 { get; set; }
        public IFormFile? GroundImage4 { get; set; }

    }
}
