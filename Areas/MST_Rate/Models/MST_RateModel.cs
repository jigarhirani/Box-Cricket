namespace BOXCricket.Areas.MST_Rate.Models
{
    public class MST_RateModel
    {
        public int? RateID { get; set; }
        public int GroundID { get; set; }
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string DayOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class MST_GroundDropDownModel
    {
        public int? GroundID { get; set; }
        public string GroundName { get; set; }
    }
}
