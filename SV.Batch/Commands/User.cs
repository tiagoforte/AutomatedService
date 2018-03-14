using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Commands
{
    public class User
    {

        public User()
        {
            this.Email = ConfigurationManager.AppSettings["USER_EMAIL"];
            this.Login = ConfigurationManager.AppSettings["USER_LOGIN"];
            this.Nome = ConfigurationManager.AppSettings["USER_NOME"];
            this.SistemaId = Guid.Parse(ConfigurationManager.AppSettings["USER_SISTEMAID"]);
            this.CodigoPerfil = Convert.ToInt32(ConfigurationManager.AppSettings["USER_PERFIL"]);
            this.CodigoGrupo = Convert.ToInt32(ConfigurationManager.AppSettings["USER_GRUPO"]);
        }

        public string Email { get; private set; }
        public string Login { get; private set; }
        public string Nome { get; private set; }
        public Guid SistemaId { get; private set; }
        public int CodigoPerfil { get; private set; }
        public int CodigoGrupo { get; private set; }
        public string Key { get; set; }
        public string Token { get; private set; }
        public void SetToken(string token)
        {
            this.Token = token;
        }
    }
}
