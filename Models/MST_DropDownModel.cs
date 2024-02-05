namespace BOXCricket.Models
{
    public class MST_DropDownModel
    {
        public class MST_BOXCricketDropDownModel
        {
            public int BOXCricketID { get; set; }
            public string? BOXCricketName { get; set; }
        }

        public class MST_GroundDropDownModel
        {
            public int GroundID { get; set; }
            public string? GroundName { get; set; }
        }

        public class MST_SlotDropDownModel
        {
            public int SlotNO { get; set; }
            public string? SlotDetails { get; set; }
        }
    }
}
