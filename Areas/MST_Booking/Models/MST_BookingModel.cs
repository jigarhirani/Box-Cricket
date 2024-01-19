namespace BOXCricket.Areas.MST_Booking.Models
{
    public class MST_BookingModel
    {
        public int? BookingID { get; set; }
        public int GroundID { get; set; }
        public string? GroundName { get; set; }
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
    public class MST_GroundDropDownModel
    {
        public int? GroundID { get; set; }
        public string GroundName { get; set; }
    }
}
