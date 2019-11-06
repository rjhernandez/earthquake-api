using System.ComponentModel.DataAnnotations;

namespace EarthquakeApi.Models
{
    public class SingleUseTokenAuthenticationRequest
    {
        [Required]        
        public string Token { get; set; }
    }
}