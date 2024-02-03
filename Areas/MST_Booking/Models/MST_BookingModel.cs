using System.ComponentModel.DataAnnotations;

namespace BOXCricket.Areas.MST_Booking.Models
{
    public class MST_BookingModel
    {
        public int? BookingID { get; set; }

        [Required(ErrorMessage = "Please choose the Ground.")]
        public int GroundID { get; set; }

        [Required(ErrorMessage = "Please Enter BookedBy.")]
        public int BookedBy { get; set; }

        [Required(ErrorMessage = "Please choose the Booking Date.")]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "Please choose the Start Time.")]
        public DateTime FromTime { get; set; }

        [Required(ErrorMessage = "Please choose the End Time.")]
        public DateTime ToTime { get; set; }

        public string? Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
