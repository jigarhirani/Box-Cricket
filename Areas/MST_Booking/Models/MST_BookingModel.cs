using System.ComponentModel.DataAnnotations;

namespace BOXCricket.Areas.MST_Booking.Models
{
    public class MST_BookingModel
    {
        public int? BookingID { get; set; }

        [Required(ErrorMessage = "Please choose the BOX Cricket.")]
        public int BOXCricketID { get; set; }

        [Required(ErrorMessage = "Please choose the Ground.")]
        public int GroundID { get; set; }

        [Required(ErrorMessage = "Please Enter BookedBy.")]
        public int BookedBy { get; set; }

        [Required(ErrorMessage = "Please choose the Booking Date.")]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "Please Enter BookingAmount.")]
        public decimal BookingAmount { get; set; }

        [Required(ErrorMessage = "Please choose the Slot.")]
        public int? SlotNO { get; set; }

        public string? Slots { get; set; }

        public string Status { get; set; }
        public string? Remarks { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class MST_BookingStatusUpdateModel
    {
        public int? BookingID { get; set; }

        public string? BOXCricketName { get; set; }
        public string? GroundName { get; set; }

        public DateTime BookingDate { get; set; }
        public decimal BookingAmount { get; set; }
        public string? SlotDetail { get; set; }

        public string Status { get; set; }
        public string? Remarks { get; set; }

        public int? TotalSlotsBooked { get; set; }
        public DateTime Modified { get; set; }

        public string? BookedBy { get; set; }
    }
}
