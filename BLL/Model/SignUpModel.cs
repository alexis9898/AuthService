using Newtonsoft.Json;

namespace BLL.Model
{
    public class SignUpModel
    {
        //public string? Email { get; set; }
        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
       // [JsonProperty("role")]
        //public string Role { get; set; }
    }
}
