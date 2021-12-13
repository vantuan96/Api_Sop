using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Api_SOP.BasicAuth.AuthAttribute
{ 
    public class AuthIdentity : GenericIdentity
    {
        public string Password { get; set; }
        public AuthIdentity(string name, string password) : base(name, "Basic")
        {
            this.Password = password;
        }
    }
}
