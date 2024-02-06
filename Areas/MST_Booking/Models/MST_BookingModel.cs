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
        public int SlotNO { get; set; }

        public string? Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
