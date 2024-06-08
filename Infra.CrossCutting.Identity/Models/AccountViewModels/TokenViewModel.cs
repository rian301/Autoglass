using System;

namespace Autoglass.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class TokenViewModel
    {
        public DateTime created { get; set; }
        public DateTime expiration { get; set; }
        public string access_token { get; set; }
        public string name { get; set; }
        public int user_id { get; set; }
        public string refresh_token { get; set; }
        public bool register_completed { get; set; }
        public string username { get; set; }
    }
}
