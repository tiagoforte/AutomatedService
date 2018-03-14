using SV.Batch.Commands;
using SV.Batch.Interface.ApiClient;
using SV.Batch.Repository.ApiClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Services
{
    public class Auth
    {        
        private IOAuth _oauth { get; set; }
        public Auth()
        {
            this._oauth = new OAuth();
        }

        public string Authenticate()
        {
            var returnKey = _oauth.GetKey(new Login(ConfigurationManager.AppSettings["USER_LOGIN"])).Result;
            if (returnKey.Success)
            {                
                var result = _oauth.Auth(new User() { Key = returnKey.Data}).Result;
                if (result.Success)
                {
                    return result.Data.access_token;
                }
            }
            return null;
        }


    }
}