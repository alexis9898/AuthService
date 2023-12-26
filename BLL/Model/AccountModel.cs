using System;

namespace BLL.Model
{
    public class AccountModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        //public string Role { get; set; }
        public string _token { get; set; }
        public DateTime _tokenExpirationDate { get; set; }
    }
}
