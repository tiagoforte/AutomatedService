using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Commands
{
    public class Login
    {
        public Login(string login)
        {
            this.login = login;
        }
        public string login { get; private set; }
    }
}
