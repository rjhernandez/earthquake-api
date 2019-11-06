namespace EarthquakeApi.Models
{
    public class AccessToken 
    {
        public string Token { get; set; }

        public static AccessToken WithToken(string token)
        {
            return new AccessToken { Token = token};
        }
    }
}