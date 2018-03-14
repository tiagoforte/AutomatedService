using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace SV.Batch
{
    [RunInstaller(true)]
    public partial class Service_SVBatchnstaller : System.Configuration.Install.Installer
    {
        public Service_SVBatchnstaller()
        {
            InitializeComponent();
        }
    }
}
