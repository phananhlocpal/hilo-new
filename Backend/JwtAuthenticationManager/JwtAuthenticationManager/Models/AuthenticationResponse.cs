using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager.Models
{
    public class AuthenticationResponse
    {
<<<<<<< HEAD
        public object Account { get; set; }
=======
        public string Email { get; set; }
>>>>>>> 960a83c (commit)
        public string JwtToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
