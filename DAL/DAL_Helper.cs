namespace BOXCricket.DAL
{
    public class DAL_Helper
    {
        public static string ConnStr = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("MyConnection");
    }
}
