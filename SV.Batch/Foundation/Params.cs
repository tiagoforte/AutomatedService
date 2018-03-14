using SV.Batch.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Foundation
{
    public class Params
    {
        private static User usuario;

        private Params() { }

        public static User Usuario
        {
            get
            {
                if (usuario == null)
                {
                    usuario = new User();
                }
                return usuario;
            }
        }

    }
}
