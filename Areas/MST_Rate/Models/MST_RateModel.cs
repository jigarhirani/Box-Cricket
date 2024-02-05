using System.ComponentModel.DataAnnotations;

namespace BOXCricket.Areas.MST_Rate.Models
{
    public class MST_RateModel
    {
        public int? RateID { get; set; }

        [Required(ErrorMessage = "Please choose the Ground.")]
        public int GroundID { get; set; }

        public int UserID { get; set; }

        [Required(ErrorMessage = "Please choose the Day Of Week.")]
        public string DayOfWeek { get; set; }

        [Required(ErrorMessage = "Please choose the Slot.")]
        public int SlotNO { get; set; }

        [Required(ErrorMessage = "Please Enter Hourly Rate in INR.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Please enter a valid amount in INR.")]
        public decimal HourlyRate { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
