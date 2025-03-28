using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBlokk.Model
{
    public class UserSession
    {
        public static string? Username { get; set; }
        public static string? QRCode { get; set; }
        public static bool IsLoggedIn => !string.IsNullOrEmpty(Username);
    }
}
