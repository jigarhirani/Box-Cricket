    namespace BOXCricket.Areas.MST_Ground.Models
{
    public class MST_GroundModel
    {
        public int? GroundID { get; set; }
        public int UserID { get; set; }        
        public string GroundName { get; set; }
        public int GroundCapacity { get; set; }
        public decimal GroundWidth { get; set; }
        public decimal GroundHeight { get; set; }
        public int CountryID { get; set; }        
        public int StateID { get; set; }
        public int CityID { get; set; }
        public string Address { get; set; }
        public bool IsAllowedBooking { get; set; } = false;
        public DateOnly Created { get; set; }
        public DateOnly Modified { get; set; }
    }
    public class MST_CountryDropDownModel
    {
        public int CountryID { get; set; }
        public string? CountryName { get; set; }
    }
    public class MST_StateDropDownModel
    {
        public int StateID { get; set; }
        public string? StateName { get; set; }
    }
    public class MST_CityDropDownModel
    {
        public int CityID { get; set; }
        public string? CityName { get; set; }
    }
}
