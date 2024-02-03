namespace BOXCricket.Models
{
    public class LOC_DropDownModel
    {
        public class LOC_CountryDropDownModel
        {
            public int CountryID { get; set; }
            public string? CountryName { get; set; }
        }
        public class LOC_StateDropDownModel
        {
            public int StateID { get; set; }
            public string? StateName { get; set; }
        }
        public class LOC_CityDropDownModel
        {
            public int CityID { get; set; }
            public string? CityName { get; set; }
        }
    }
}
